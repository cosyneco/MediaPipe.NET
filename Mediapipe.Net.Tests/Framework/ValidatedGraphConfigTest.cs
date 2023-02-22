// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System.Linq;
using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Packets;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Framework.ValidatedGraphConfig;
using NUnit.Framework;

namespace Mediapipe.Net.Tests
{
    public class ValidatedGraphConfigTest
    {
        private const string pass_through_config_text = @"
node {
  calculator: ""PassThroughCalculator""
  input_stream: ""in""
  output_stream: ""out1""
}
node {
  calculator: ""PassThroughCalculator""
  input_stream: ""out1""
  output_stream: ""out""
}
input_stream: ""in""
output_stream: ""out""
";

        private const string flow_limiter_config_text = @"
input_stream: ""input_video""
input_stream: ""output""

node {
  calculator: ""FlowLimiterCalculator""
  input_stream: ""input_video""
  input_stream: ""FINISHED:output""
  input_stream_info: {
    tag_index: ""FINISHED""
    back_edge: true
  }
  input_side_packet: ""MAX_IN_FLIGHT:max_in_flight""
  input_side_packet: ""OPTIONS:flow_limiter_calculator_options""
  output_stream: ""throttled_input_video""
}
";

        private const string image_transformation_config_text = @"
input_stream: ""input_video""

node: {
  calculator: ""ImageTransformationCalculator""
  input_stream: ""IMAGE:input_video""
  input_side_packet: ""ROTATION_DEGREES:input_rotation""
  input_side_packet: ""FLIP_HORIZONTALLY:input_horizontally_flipped""
  input_side_packet: ""FLIP_VERTICALLY:input_vertically_flipped""
  output_stream: ""IMAGE:transformed_input_video""
}
";

        private const string constant_side_packet_config_text = @"
node {
  calculator: ""ConstantSidePacketCalculator""
  output_side_packet: ""PACKET:0:int_packet""
  output_side_packet: ""PACKET:1:float_packet""
  output_side_packet: ""PACKET:2:bool_packet""
  output_side_packet: ""PACKET:3:string_packet""
  options: {
    [mediapipe.ConstantSidePacketCalculatorOptions.ext]: {
      packet { int_value: 256 }
      packet { float_value: 0.5f }
      packet { bool_value: false }
      packet { string_value: ""string"" }
    }
  }
}
";

        private const string face_detection_short_range_common_config_text = @"
input_stream: ""image""
input_stream: ""roi""
node {
  calculator: ""FaceDetectionShortRange""
  input_stream: ""IMAGE:image""
  input_stream: ""ROI:roi""
  output_stream: ""DETECTIONS:detections""
}
";

        #region Constructor
        [Test]
        public void Ctor_ShouldInstantiateValidatedGraphConfig()
        {
            Assert.DoesNotThrow(() =>
            {
                var config = new ValidatedGraphConfig();
                config.Dispose();
            });
        }
        #endregion

        #region #IsDisposed
        [Test]
        public void IsDisposed_ShouldReturnFalse_When_NotDisposedYet()
        {
            using (var config = new ValidatedGraphConfig())
            {
                Assert.False(config.IsDisposed);
            }
        }

        [Test]
        public void IsDisposed_ShouldReturnTrue_When_AlreadyDisposed()
        {
            var config = new ValidatedGraphConfig();
            config.Dispose();

            Assert.True(config.IsDisposed);
        }
        #endregion

        #region #Initialize
        [Test]
        public void Initialize_ShouldReturnOk_When_CalledWithConfig()
        {
            using (var config = new ValidatedGraphConfig())
            {
                using (var status = config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(pass_through_config_text)))
                {
                    Assert.True(status.Ok());
                }
                Assert.True(config.Initialized());
            }
        }

        [Test]
        public void Initialize_ShouldReturnOk_When_CalledWithValidGraphType()
        {
            using (var config = new ValidatedGraphConfig())
            {
                using (var status = config.Initialize("SwitchContainer"))
                {
                    Assert.True(status.Ok());
                }
                Assert.True(config.Initialized());
            }
        }

        [Test]
        public void Initialize_ShouldReturnInternalError_When_CalledWithInvalidGraphType()
        {
            using (var config = new ValidatedGraphConfig())
            {
                using (var status = config.Initialize("InvalidSubgraph"))
                {
                    Assert.AreEqual(Status.StatusCode.NotFound, status.Code);
                }
                Assert.False(config.Initialized());
            }
        }
        #endregion

        #region #ValidateRequiredSidePackets
        [Test]
        public void ValidateRequiredSidePackets_ShouldReturnOk_When_TheConfigDoesNotRequireSidePackets_And_SidePacketIsEmpty()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(pass_through_config_text)).AssertOk();
                using (var sidePackets = new SidePacket())
                {
                    using (var status = config.ValidateRequiredSidePackets(sidePackets))
                    {
                        Assert.True(status.Ok());
                    }
                }
            }
        }

        [Test]
        public void ValidateRequiredSidePackets_ShouldReturnOk_When_TheConfigDoesNotRequireSidePackets_And_SidePacketIsNotEmpty()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(pass_through_config_text)).AssertOk();
                using (var sidePackets = new SidePacket())
                {
                    sidePackets.Emplace("in", new IntPacket(0));
                    using (var status = config.ValidateRequiredSidePackets(sidePackets))
                    {
                        Assert.True(status.Ok());
                    }
                }
            }
        }

        [Test]
        public void ValidateRequiredSidePackets_ShouldReturnOk_When_AllTheSidePacketsAreOptional_And_SidePacketIsEmpty()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(flow_limiter_config_text)).AssertOk();
                using (var sidePackets = new SidePacket())
                {
                    using (var status = config.ValidateRequiredSidePackets(sidePackets))
                    {
                        Assert.True(status.Ok());
                    }
                }
            }
        }

        [Test]
        public void ValidateRequiredSidePackets_ShouldReturnInvalidArgumentError_When_TheConfigRequiresSidePackets_And_SidePacketIsEmpty()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(image_transformation_config_text)).AssertOk();
                using (var sidePackets = new SidePacket())
                {
                    using (var status = config.ValidateRequiredSidePackets(sidePackets))
                    {
                        Assert.AreEqual(Status.StatusCode.InvalidArgument, status.Code);
                    }
                }
            }
        }

        [Test]
        public void ValidateRequiredSidePackets_ShouldReturnInvalidArgumentError_When_AllTheRequiredSidePacketsAreNotGiven()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(image_transformation_config_text)).AssertOk();
                using (var sidePackets = new SidePacket())
                {
                    sidePackets.Emplace("input_horizontally_flipped", new BoolPacket(false));
                    sidePackets.Emplace("input_vertically_flipped", new BoolPacket(true));
                    using (var status = config.ValidateRequiredSidePackets(sidePackets))
                    {
                        Assert.AreEqual(Status.StatusCode.InvalidArgument, status.Code);
                    }
                }
            }
        }

        [Test]
        public void ValidateRequiredSidePackets_ShouldReturnInvalidArgumentError_When_TheSidePacketValuesAreWrong()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(image_transformation_config_text)).AssertOk();
                using (var sidePackets = new SidePacket())
                {
                    sidePackets.Emplace("input_horizontally_flipped", new BoolPacket(false));
                    sidePackets.Emplace("input_vertically_flipped", new BoolPacket(true));
                    sidePackets.Emplace("input_rotation", new StringPacket("0"));
                    using (var status = config.ValidateRequiredSidePackets(sidePackets))
                    {
                        Assert.AreEqual(Status.StatusCode.InvalidArgument, status.Code);
                    }
                }
            }
        }

        [Test]
        public void ValidateRequiredSidePackets_ShouldReturnOk_When_AllTheRequiredSidePacketsAreGiven()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(image_transformation_config_text)).AssertOk();
                using (var sidePackets = new SidePacket())
                {
                    sidePackets.Emplace("input_horizontally_flipped", new BoolPacket(false));
                    sidePackets.Emplace("input_vertically_flipped", new BoolPacket(true));
                    sidePackets.Emplace("input_rotation", new IntPacket(0));
                    using (var status = config.ValidateRequiredSidePackets(sidePackets))
                    {
                        Assert.True(status.Ok());
                    }
                }
            }
        }
        #endregion

        #region Config
        [Test]
        public void Config_ShouldReturnAnEmptyConfig_When_NotInitialized()
        {
            using (var config = new ValidatedGraphConfig())
            {
                var canonicalizedConfig = config.Config();
                Assert.AreEqual(canonicalizedConfig.CalculateSize(), 0);
            }
        }

        [Test]
        public void Config_ShouldReturnTheCanonicalizedConfig_When_TheConfigIsPassThroughConfig()
        {
            using (var config = new ValidatedGraphConfig())
            {
                var originalConfig = CalculatorGraphConfig.Parser.ParseFromTextFormat(pass_through_config_text);
                config.Initialize(originalConfig).AssertOk();
                var canonicalizedConfig = config.Config();

                Assert.AreEqual(originalConfig.Node, canonicalizedConfig.Node);
                Assert.AreEqual(originalConfig.InputStream, canonicalizedConfig.InputStream);
                Assert.AreEqual(originalConfig.OutputStream, canonicalizedConfig.OutputStream);
                Assert.IsEmpty(originalConfig.Executor);
                Assert.AreEqual(1, canonicalizedConfig.Executor.Count);
                Assert.AreEqual(0, canonicalizedConfig.Executor[0].CalculateSize());

                Assert.AreEqual(80, originalConfig.CalculateSize());
                Assert.AreEqual(82, canonicalizedConfig.CalculateSize());
            }
        }

        [Test]
        public void Config_ShouldReturnTheCanonicalizedConfig_When_TheConfigIsFaceDetectionShortRangeCommonConfig()
        {
            using (var config = new ValidatedGraphConfig())
            {
                var originalConfig = CalculatorGraphConfig.Parser.ParseFromTextFormat(face_detection_short_range_common_config_text);
                config.Initialize(originalConfig).AssertOk();
                var canonicalizedConfig = config.Config();

                Assert.AreEqual(84, originalConfig.CalculateSize());
                // This is different depending on the runtime
                // In this case, we're testing for 2167, the size for the CPU runtime, and 2166 for the GPU runtime.
                Assert.AreEqual(2167, canonicalizedConfig.CalculateSize());
            }
        }
        #endregion

        #region InputStreamInfos
        [Test]
        public void InputStreamInfos_ShouldReturnEmptyList_When_NotInitialized()
        {
            using (var config = new ValidatedGraphConfig())
            {
                Assert.IsEmpty(config.InputStreamInfos());
            }
        }

        [Test]
        public void InputStreamInfos_ShouldReturnEmptyList_When_NoInputStreamExists()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(constant_side_packet_config_text)).AssertOk();
                Assert.IsEmpty(config.InputStreamInfos());
            }
        }

        [Test]
        public void InputStreamInfos_ShouldReturnEdgeInfoList_When_InputStreamsExist()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(pass_through_config_text)).AssertOk();
                var inputStreamInfos = config.InputStreamInfos();

                Assert.AreEqual(inputStreamInfos.Count, 2);

                var inStream = inputStreamInfos.First((edgeInfo) => edgeInfo.Name == "in");
                Assert.AreEqual(0, inStream.Upstream);
                Assert.AreEqual(NodeType.Calculator, inStream.ParentNode.Type);
                Assert.AreEqual(0, inStream.ParentNode.Index);
                Assert.False(inStream.BackEdge);

                var out1Stream = inputStreamInfos.First((edgeInfo) => edgeInfo.Name == "out1");
                Assert.AreEqual(1, out1Stream.Upstream);
                Assert.AreEqual(NodeType.Calculator, out1Stream.ParentNode.Type);
                Assert.AreEqual(1, out1Stream.ParentNode.Index);
                Assert.False(out1Stream.BackEdge);
            }
        }
        #endregion

        #region OutputStreamInfos
        [Test]
        public void OutputStreamInfos_ShouldReturnEmptyList_When_NotInitialized()
        {
            using (var config = new ValidatedGraphConfig())
            {
                Assert.IsEmpty(config.OutputStreamInfos());
            }
        }

        [Test]
        public void OutputStreamInfos_ShouldReturnEdgeInfoList_When_OutputStreamsExist()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(pass_through_config_text)).AssertOk();
                var outputStreamInfos = config.OutputStreamInfos();

                Assert.AreEqual(3, outputStreamInfos.Count);

                var inStream = outputStreamInfos.First((edgeInfo) => edgeInfo.Name == "in");
                Assert.AreEqual(-1, inStream.Upstream);
                Assert.AreEqual(NodeType.GraphInputStream, inStream.ParentNode.Type);
                Assert.AreEqual(2, inStream.ParentNode.Index, 2);
                Assert.False(inStream.BackEdge);

                var out1Stream = outputStreamInfos.First((edgeInfo) => edgeInfo.Name == "out1");
                Assert.AreEqual(-1, out1Stream.Upstream);
                Assert.AreEqual(NodeType.Calculator, out1Stream.ParentNode.Type);
                Assert.AreEqual(0, out1Stream.ParentNode.Index);
                Assert.False(out1Stream.BackEdge);

                var outStream = outputStreamInfos.First((edgeInfo) => edgeInfo.Name == "out");
                Assert.AreEqual(-1, outStream.Upstream);
                Assert.AreEqual(NodeType.Calculator, outStream.ParentNode.Type);
                Assert.AreEqual(1, outStream.ParentNode.Index);
                Assert.False(outStream.BackEdge);
            }
        }
        #endregion

        #region InputSidePacketInfos
        [Test]
        public void InputSidePacketInfos_ShouldReturnEmptyList_When_NotInitialized()
        {
            using (var config = new ValidatedGraphConfig())
            {
                Assert.IsEmpty(config.InputSidePacketInfos());
            }
        }

        [Test]
        public void InputSidePacketInfos_ShouldReturnEmptyList_When_NoInputSidePacketExists()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(pass_through_config_text)).AssertOk();
                Assert.IsEmpty(config.InputSidePacketInfos());
            }
        }

        [Test]
        public void InputSidePacketInfos_ShouldReturnEdgeInfoList_When_InputSidePacketsExist()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(flow_limiter_config_text)).AssertOk();
                var inputSidePacketInfos = config.InputSidePacketInfos();

                Assert.True(inputSidePacketInfos.Count >= 2);

                var maxInFlightPacket = inputSidePacketInfos.First((edgeInfo) => edgeInfo.Name == "max_in_flight");
                Assert.AreEqual(-1, maxInFlightPacket.Upstream);
                Assert.AreEqual(NodeType.Calculator, maxInFlightPacket.ParentNode.Type);
                Assert.False(maxInFlightPacket.BackEdge);

                var flowLimiterCalculatorOptionsPacket = inputSidePacketInfos.First((edgeInfo) => edgeInfo.Name == "flow_limiter_calculator_options");
                Assert.AreEqual(-1, flowLimiterCalculatorOptionsPacket.Upstream);
                Assert.AreEqual(NodeType.Calculator, flowLimiterCalculatorOptionsPacket.ParentNode.Type);
                Assert.False(flowLimiterCalculatorOptionsPacket.BackEdge);
            }
        }
        #endregion

        #region OutputSidePacketInfos
        [Test]
        public void OutputSidePacketInfos_ShouldReturnEmptyList_When_NotInitialized()
        {
            using (var config = new ValidatedGraphConfig())
            {
                Assert.IsEmpty(config.OutputSidePacketInfos());
            }
        }

        [Test]
        public void OutputSidePacketInfos_ShouldReturnEmptyList_When_NoOutputSidePacketExists()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(pass_through_config_text)).AssertOk();
                Assert.IsEmpty(config.OutputSidePacketInfos());
            }
        }

        [Test]
        public void OutputSidePacketInfos_ShouldReturnEdgeInfoList_When_OutputSidePacketsExist()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(constant_side_packet_config_text)).AssertOk();
                var outputSidePacketInfos = config.OutputSidePacketInfos();

                Assert.AreEqual(4, outputSidePacketInfos.Count);

                var intPacket = outputSidePacketInfos.First((edgeInfo) => edgeInfo.Name == "int_packet");
                Assert.AreEqual(-1, intPacket.Upstream);
                Assert.AreEqual(NodeType.Calculator, intPacket.ParentNode.Type);
                Assert.False(intPacket.BackEdge);

                var floatPacket = outputSidePacketInfos.First((edgeInfo) => edgeInfo.Name == "float_packet");
                Assert.AreEqual(-1, floatPacket.Upstream);
                Assert.AreEqual(NodeType.Calculator, floatPacket.ParentNode.Type);
                Assert.False(floatPacket.BackEdge);

                var boolPacket = outputSidePacketInfos.First((edgeInfo) => edgeInfo.Name == "bool_packet");
                Assert.AreEqual(-1, boolPacket.Upstream);
                Assert.AreEqual(NodeType.Calculator, boolPacket.ParentNode.Type);
                Assert.False(boolPacket.BackEdge);

                var stringPacket = outputSidePacketInfos.First((edgeInfo) => edgeInfo.Name == "string_packet");
                Assert.AreEqual(-1, stringPacket.Upstream);
                Assert.AreEqual(NodeType.Calculator, stringPacket.ParentNode.Type);
                Assert.False(stringPacket.BackEdge);
            }
        }
        #endregion

        #region OutputStreamIndex
        [Test]
        public void OutputStreamIndex_ShouldReturnNegativeValue_When_NotInitialized()
        {
            using (var config = new ValidatedGraphConfig())
            {
                Assert.AreEqual(-1, config.OutputStreamIndex(""));
            }
        }

        [Test]
        public void OutputStreamIndex_ShouldReturnNegativeValue_When_TheNameIsInvalid()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(pass_through_config_text)).AssertOk();
                Assert.AreEqual(-1, config.OutputStreamIndex("unknown"));
            }
        }

        [Test]
        public void OutputStreamIndex_ShouldReturnIndex_When_TheNameIsValid()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(pass_through_config_text)).AssertOk();
                Assert.AreEqual(2, config.OutputStreamIndex("out"));
            }
        }

        [Test]
        public void OutputStreamIndex_ShouldReturnIndex_When_TheStreamIsNotPublic()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(pass_through_config_text)).AssertOk();
                Assert.AreEqual(1, config.OutputStreamIndex("out1"));
            }
        }
        #endregion

        #region OutputSidePacketIndex
        [Test]
        public void OutputSidePacketIndex_ShouldReturnNegativeValue_When_NotInitialized()
        {
            using (var config = new ValidatedGraphConfig())
            {
                Assert.AreEqual(-1, config.OutputSidePacketIndex(""));
            }
        }

        [Test]
        public void OutputSidePacketIndex_ShouldReturnNegativeValue_When_TheNameIsInvalid()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(constant_side_packet_config_text)).AssertOk();
                Assert.AreEqual(-1, config.OutputSidePacketIndex("unknown"));
            }
        }

        [Test]
        public void OutputSidePacketIndex_ShouldReturnIndex_When_TheNameIsValid()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(constant_side_packet_config_text)).AssertOk();
                Assert.AreEqual(0, config.OutputSidePacketIndex("int_packet"));
            }
        }
        #endregion


        #region OutputStreamToNode
        [Test]
        public void OutputStreamToNode_ShouldReturnNegativeValue_When_NotInitialized()
        {
            using (var config = new ValidatedGraphConfig())
            {
                Assert.AreEqual(-1, config.OutputStreamToNode(""));
            }
        }

        [Test]
        public void OutputStreamToNode_ShouldReturnNegativeValue_When_TheNameIsInvalid()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(pass_through_config_text)).AssertOk();
                Assert.AreEqual(-1, config.OutputStreamToNode("unknown"));
            }
        }

        [Test]
        public void OutputStreamToNode_ShouldReturnIndex_When_TheNameIsValid()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(pass_through_config_text)).AssertOk();
                Assert.AreEqual(0, config.OutputStreamToNode("out1"));
            }
        }
        #endregion

        #region RegisteredSidePacketTypeName
        [Test]
        public void RegisteredSidePacketTypeName_ShouldReturnInvalidArgumentError_When_TheSidePacketDoesNotExist()
        {
            using (var config = new ValidatedGraphConfig())
            {
                using (var statusOrString = config.RegisteredSidePacketTypeName("max_in_flight"))
                {
                    Assert.AreEqual(Status.StatusCode.InvalidArgument, statusOrString.Status.Code);
                }
            }
        }

        [Test]
        public void RegisteredSidePacketTypeName_ShouldReturnUnknownError_When_TheSidePacketTypeCannotBeDetermined()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(flow_limiter_config_text)).AssertOk();
                using (var statusOrString = config.RegisteredSidePacketTypeName("max_in_flight"))
                {
                    Assert.AreEqual(Status.StatusCode.Unknown, statusOrString.Status.Code);
                }
            }
        }
        #endregion

        #region RegisteredStreamTypeName
        [Test]
        public void RegisteredStreamTypeName_ShouldReturnInvalidArgumentError_When_TheStreamDoesNotExist()
        {
            using (var config = new ValidatedGraphConfig())
            {
                using (var statusOrString = config.RegisteredStreamTypeName("in"))
                {
                    Assert.AreEqual(Status.StatusCode.InvalidArgument, statusOrString.Status.Code);
                }
            }
        }

        [Test]
        public void RegisteredStreamTypeName_ShouldReturnUnknownError_When_TheStreamTypeCannotBeDetermined()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(pass_through_config_text)).AssertOk();
                using (var statusOrString = config.RegisteredStreamTypeName("in"))
                {
                    Assert.AreEqual(Status.StatusCode.Unknown, statusOrString.Status.Code);
                }
            }
        }
        #endregion

        #region Package
        [Test]
        public void Package_ShouldReturnNull_When_NotInitialized()
        {
            using var config = new ValidatedGraphConfig();
            Assert.IsNull(config.Package());
        }

        [Test]
        public void Package_ShouldReturnNull_When_TheNamespaceIsNotSet()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(pass_through_config_text)).AssertOk();
                Assert.IsNull(config.Package());
            }
        }
        #endregion

        #region IsReservedExecutorName
        [Test]
        public void IsReservedExecutorName_ShouldReturnFalse_When_TheNameIsNotReserved()
        {
            Assert.False(ValidatedGraphConfig.IsReservedExecutorName("unknown"));
        }

        [Test]
        public void IsReservedExecutorName_ShouldReturnFalse_When_TheNameIsReserved()
        {
            Assert.True(ValidatedGraphConfig.IsReservedExecutorName("default"));
            Assert.True(ValidatedGraphConfig.IsReservedExecutorName("gpu"));
            Assert.True(ValidatedGraphConfig.IsReservedExecutorName("__gpu"));
        }
        #endregion

        #region IsExternalSidePacket
        [Test]
        public void IsExternalSidePacket_ShouldReturnFalse_When_NotInitialized()
        {
            using (var config = new ValidatedGraphConfig())
            {
                Assert.False(config.IsExternalSidePacket("max_in_flight"));
            }
        }


        [Test]
        public void IsExternalSidePacket_ShouldReturnFalse_When_TheSidePacketIsInternal()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(constant_side_packet_config_text)).AssertOk();
                Assert.False(config.IsExternalSidePacket("int_packet"));
            }
        }

        [Test]
        public void IsExternalSidePacket_ShouldReturnTrue_When_TheSidePacketIsExternal()
        {
            using (var config = new ValidatedGraphConfig())
            {
                config.Initialize(CalculatorGraphConfig.Parser.ParseFromTextFormat(flow_limiter_config_text)).AssertOk();
                Assert.True(config.IsExternalSidePacket("max_in_flight"));
            }
        }
        #endregion
    }
}
