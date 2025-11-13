using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/Found Object Event")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "Found Object Event", message: "[Agent] Found [Object] with [Tag]", category: "Events", id: "330ae376f6cecaf5df3d273f7746bf64")]
public sealed partial class FoundObjectEvent : EventChannel<GameObject, GameObject, string> { }

