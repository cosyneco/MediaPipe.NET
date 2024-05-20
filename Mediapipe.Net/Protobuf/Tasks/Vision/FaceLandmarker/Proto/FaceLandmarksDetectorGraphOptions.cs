// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: mediapipe/tasks/cc/vision/face_landmarker/proto/face_landmarks_detector_graph_options.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Mediapipe.Tasks.Vision.FaceLandmarker.Proto {

  /// <summary>Holder for reflection information generated from mediapipe/tasks/cc/vision/face_landmarker/proto/face_landmarks_detector_graph_options.proto</summary>
  public static partial class FaceLandmarksDetectorGraphOptionsReflection {

    #region Descriptor
    /// <summary>File descriptor for mediapipe/tasks/cc/vision/face_landmarker/proto/face_landmarks_detector_graph_options.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static FaceLandmarksDetectorGraphOptionsReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ClttZWRpYXBpcGUvdGFza3MvY2MvdmlzaW9uL2ZhY2VfbGFuZG1hcmtlci9w",
            "cm90by9mYWNlX2xhbmRtYXJrc19kZXRlY3Rvcl9ncmFwaF9vcHRpb25zLnBy",
            "b3RvEixtZWRpYXBpcGUudGFza3MudmlzaW9uLmZhY2VfbGFuZG1hcmtlci5w",
            "cm90bxokbWVkaWFwaXBlL2ZyYW1ld29yay9jYWxjdWxhdG9yLnByb3RvGixt",
            "ZWRpYXBpcGUvZnJhbWV3b3JrL2NhbGN1bGF0b3Jfb3B0aW9ucy5wcm90bxow",
            "bWVkaWFwaXBlL3Rhc2tzL2NjL2NvcmUvcHJvdG8vYmFzZV9vcHRpb25zLnBy",
            "b3RvGlRtZWRpYXBpcGUvdGFza3MvY2MvdmlzaW9uL2ZhY2VfbGFuZG1hcmtl",
            "ci9wcm90by9mYWNlX2JsZW5kc2hhcGVzX2dyYXBoX29wdGlvbnMucHJvdG8i",
            "lgMKIUZhY2VMYW5kbWFya3NEZXRlY3RvckdyYXBoT3B0aW9ucxI9CgxiYXNl",
            "X29wdGlvbnMYASABKAsyJy5tZWRpYXBpcGUudGFza3MuY29yZS5wcm90by5C",
            "YXNlT3B0aW9ucxIlChhtaW5fZGV0ZWN0aW9uX2NvbmZpZGVuY2UYAiABKAI6",
            "AzAuNRIYChBzbW9vdGhfbGFuZG1hcmtzGAQgASgIEnEKHmZhY2VfYmxlbmRz",
            "aGFwZXNfZ3JhcGhfb3B0aW9ucxgDIAEoCzJJLm1lZGlhcGlwZS50YXNrcy52",
            "aXNpb24uZmFjZV9sYW5kbWFya2VyLnByb3RvLkZhY2VCbGVuZHNoYXBlc0dy",
            "YXBoT3B0aW9uczJ+CgNleHQSHC5tZWRpYXBpcGUuQ2FsY3VsYXRvck9wdGlv",
            "bnMY1fnY8gEgASgLMk8ubWVkaWFwaXBlLnRhc2tzLnZpc2lvbi5mYWNlX2xh",
            "bmRtYXJrZXIucHJvdG8uRmFjZUxhbmRtYXJrc0RldGVjdG9yR3JhcGhPcHRp",
            "b25zQmAKNmNvbS5nb29nbGUubWVkaWFwaXBlLnRhc2tzLnZpc2lvbi5mYWNl",
            "bGFuZG1hcmtlci5wcm90b0ImRmFjZUxhbmRtYXJrc0RldGVjdG9yR3JhcGhP",
            "cHRpb25zUHJvdG8="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Mediapipe.CalculatorReflection.Descriptor, global::Mediapipe.CalculatorOptionsReflection.Descriptor, global::Mediapipe.Tasks.Core.Proto.BaseOptionsReflection.Descriptor, global::Mediapipe.Tasks.Vision.FaceLandmarker.Proto.FaceBlendshapesGraphOptionsReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Mediapipe.Tasks.Vision.FaceLandmarker.Proto.FaceLandmarksDetectorGraphOptions), global::Mediapipe.Tasks.Vision.FaceLandmarker.Proto.FaceLandmarksDetectorGraphOptions.Parser, new[]{ "BaseOptions", "MinDetectionConfidence", "SmoothLandmarks", "FaceBlendshapesGraphOptions" }, null, null, new pb::Extension[] { global::Mediapipe.Tasks.Vision.FaceLandmarker.Proto.FaceLandmarksDetectorGraphOptions.Extensions.Ext }, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class FaceLandmarksDetectorGraphOptions : pb::IMessage<FaceLandmarksDetectorGraphOptions>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<FaceLandmarksDetectorGraphOptions> _parser = new pb::MessageParser<FaceLandmarksDetectorGraphOptions>(() => new FaceLandmarksDetectorGraphOptions());
    private pb::UnknownFieldSet _unknownFields;
    private int _hasBits0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<FaceLandmarksDetectorGraphOptions> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Mediapipe.Tasks.Vision.FaceLandmarker.Proto.FaceLandmarksDetectorGraphOptionsReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public FaceLandmarksDetectorGraphOptions() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public FaceLandmarksDetectorGraphOptions(FaceLandmarksDetectorGraphOptions other) : this() {
      _hasBits0 = other._hasBits0;
      baseOptions_ = other.baseOptions_ != null ? other.baseOptions_.Clone() : null;
      minDetectionConfidence_ = other.minDetectionConfidence_;
      smoothLandmarks_ = other.smoothLandmarks_;
      faceBlendshapesGraphOptions_ = other.faceBlendshapesGraphOptions_ != null ? other.faceBlendshapesGraphOptions_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public FaceLandmarksDetectorGraphOptions Clone() {
      return new FaceLandmarksDetectorGraphOptions(this);
    }

    /// <summary>Field number for the "base_options" field.</summary>
    public const int BaseOptionsFieldNumber = 1;
    private global::Mediapipe.Tasks.Core.Proto.BaseOptions baseOptions_;
    /// <summary>
    /// Base options for configuring Task library, such as specifying the TfLite
    /// model file with metadata, accelerator options, etc.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Mediapipe.Tasks.Core.Proto.BaseOptions BaseOptions {
      get { return baseOptions_; }
      set {
        baseOptions_ = value;
      }
    }

    /// <summary>Field number for the "min_detection_confidence" field.</summary>
    public const int MinDetectionConfidenceFieldNumber = 2;
    private readonly static float MinDetectionConfidenceDefaultValue = 0.5F;

    private float minDetectionConfidence_;
    /// <summary>
    /// Minimum confidence value ([0.0, 1.0]) for confidence score to be considered
    /// successfully detecting a face in the image.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public float MinDetectionConfidence {
      get { if ((_hasBits0 & 1) != 0) { return minDetectionConfidence_; } else { return MinDetectionConfidenceDefaultValue; } }
      set {
        _hasBits0 |= 1;
        minDetectionConfidence_ = value;
      }
    }
    /// <summary>Gets whether the "min_detection_confidence" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasMinDetectionConfidence {
      get { return (_hasBits0 & 1) != 0; }
    }
    /// <summary>Clears the value of the "min_detection_confidence" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearMinDetectionConfidence() {
      _hasBits0 &= ~1;
    }

    /// <summary>Field number for the "smooth_landmarks" field.</summary>
    public const int SmoothLandmarksFieldNumber = 4;
    private readonly static bool SmoothLandmarksDefaultValue = false;

    private bool smoothLandmarks_;
    /// <summary>
    /// Whether to smooth the detected landmarks over timestamps. Note that
    /// landmarks smoothing is only applicable for a single face. If multiple faces
    /// landmarks are given, and smooth_landmarks is true, only the first face
    /// landmarks would be smoothed, and the remaining landmarks are discarded in
    /// the returned landmarks list.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool SmoothLandmarks {
      get { if ((_hasBits0 & 2) != 0) { return smoothLandmarks_; } else { return SmoothLandmarksDefaultValue; } }
      set {
        _hasBits0 |= 2;
        smoothLandmarks_ = value;
      }
    }
    /// <summary>Gets whether the "smooth_landmarks" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasSmoothLandmarks {
      get { return (_hasBits0 & 2) != 0; }
    }
    /// <summary>Clears the value of the "smooth_landmarks" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearSmoothLandmarks() {
      _hasBits0 &= ~2;
    }

    /// <summary>Field number for the "face_blendshapes_graph_options" field.</summary>
    public const int FaceBlendshapesGraphOptionsFieldNumber = 3;
    private global::Mediapipe.Tasks.Vision.FaceLandmarker.Proto.FaceBlendshapesGraphOptions faceBlendshapesGraphOptions_;
    /// <summary>
    /// Optional options for FaceBlendshapeGraph. If this options is set, the
    /// FaceLandmarksDetectorGraph would output the face blendshapes.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Mediapipe.Tasks.Vision.FaceLandmarker.Proto.FaceBlendshapesGraphOptions FaceBlendshapesGraphOptions {
      get { return faceBlendshapesGraphOptions_; }
      set {
        faceBlendshapesGraphOptions_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as FaceLandmarksDetectorGraphOptions);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(FaceLandmarksDetectorGraphOptions other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(BaseOptions, other.BaseOptions)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(MinDetectionConfidence, other.MinDetectionConfidence)) return false;
      if (SmoothLandmarks != other.SmoothLandmarks) return false;
      if (!object.Equals(FaceBlendshapesGraphOptions, other.FaceBlendshapesGraphOptions)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (baseOptions_ != null) hash ^= BaseOptions.GetHashCode();
      if (HasMinDetectionConfidence) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(MinDetectionConfidence);
      if (HasSmoothLandmarks) hash ^= SmoothLandmarks.GetHashCode();
      if (faceBlendshapesGraphOptions_ != null) hash ^= FaceBlendshapesGraphOptions.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (baseOptions_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(BaseOptions);
      }
      if (HasMinDetectionConfidence) {
        output.WriteRawTag(21);
        output.WriteFloat(MinDetectionConfidence);
      }
      if (faceBlendshapesGraphOptions_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(FaceBlendshapesGraphOptions);
      }
      if (HasSmoothLandmarks) {
        output.WriteRawTag(32);
        output.WriteBool(SmoothLandmarks);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (baseOptions_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(BaseOptions);
      }
      if (HasMinDetectionConfidence) {
        output.WriteRawTag(21);
        output.WriteFloat(MinDetectionConfidence);
      }
      if (faceBlendshapesGraphOptions_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(FaceBlendshapesGraphOptions);
      }
      if (HasSmoothLandmarks) {
        output.WriteRawTag(32);
        output.WriteBool(SmoothLandmarks);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (baseOptions_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(BaseOptions);
      }
      if (HasMinDetectionConfidence) {
        size += 1 + 4;
      }
      if (HasSmoothLandmarks) {
        size += 1 + 1;
      }
      if (faceBlendshapesGraphOptions_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(FaceBlendshapesGraphOptions);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(FaceLandmarksDetectorGraphOptions other) {
      if (other == null) {
        return;
      }
      if (other.baseOptions_ != null) {
        if (baseOptions_ == null) {
          BaseOptions = new global::Mediapipe.Tasks.Core.Proto.BaseOptions();
        }
        BaseOptions.MergeFrom(other.BaseOptions);
      }
      if (other.HasMinDetectionConfidence) {
        MinDetectionConfidence = other.MinDetectionConfidence;
      }
      if (other.HasSmoothLandmarks) {
        SmoothLandmarks = other.SmoothLandmarks;
      }
      if (other.faceBlendshapesGraphOptions_ != null) {
        if (faceBlendshapesGraphOptions_ == null) {
          FaceBlendshapesGraphOptions = new global::Mediapipe.Tasks.Vision.FaceLandmarker.Proto.FaceBlendshapesGraphOptions();
        }
        FaceBlendshapesGraphOptions.MergeFrom(other.FaceBlendshapesGraphOptions);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            if (baseOptions_ == null) {
              BaseOptions = new global::Mediapipe.Tasks.Core.Proto.BaseOptions();
            }
            input.ReadMessage(BaseOptions);
            break;
          }
          case 21: {
            MinDetectionConfidence = input.ReadFloat();
            break;
          }
          case 26: {
            if (faceBlendshapesGraphOptions_ == null) {
              FaceBlendshapesGraphOptions = new global::Mediapipe.Tasks.Vision.FaceLandmarker.Proto.FaceBlendshapesGraphOptions();
            }
            input.ReadMessage(FaceBlendshapesGraphOptions);
            break;
          }
          case 32: {
            SmoothLandmarks = input.ReadBool();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            if (baseOptions_ == null) {
              BaseOptions = new global::Mediapipe.Tasks.Core.Proto.BaseOptions();
            }
            input.ReadMessage(BaseOptions);
            break;
          }
          case 21: {
            MinDetectionConfidence = input.ReadFloat();
            break;
          }
          case 26: {
            if (faceBlendshapesGraphOptions_ == null) {
              FaceBlendshapesGraphOptions = new global::Mediapipe.Tasks.Vision.FaceLandmarker.Proto.FaceBlendshapesGraphOptions();
            }
            input.ReadMessage(FaceBlendshapesGraphOptions);
            break;
          }
          case 32: {
            SmoothLandmarks = input.ReadBool();
            break;
          }
        }
      }
    }
    #endif

    #region Extensions
    /// <summary>Container for extensions for other messages declared in the FaceLandmarksDetectorGraphOptions message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static partial class Extensions {
      public static readonly pb::Extension<global::Mediapipe.CalculatorOptions, global::Mediapipe.Tasks.Vision.FaceLandmarker.Proto.FaceLandmarksDetectorGraphOptions> Ext =
        new pb::Extension<global::Mediapipe.CalculatorOptions, global::Mediapipe.Tasks.Vision.FaceLandmarker.Proto.FaceLandmarksDetectorGraphOptions>(508968149, pb::FieldCodec.ForMessage(4071745194, global::Mediapipe.Tasks.Vision.FaceLandmarker.Proto.FaceLandmarksDetectorGraphOptions.Parser));
    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code