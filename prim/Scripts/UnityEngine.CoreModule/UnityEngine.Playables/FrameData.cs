using System;

namespace UnityEngine.Playables;

public struct FrameData
{
	[Flags]
	internal enum Flags
	{
		Evaluate = 1,
		SeekOccured = 2,
		Loop = 4,
		Hold = 8
	}

	public enum EvaluationType
	{
		Evaluate,
		Playback
	}

	internal ulong m_FrameID;

	internal double m_DeltaTime;

	internal float m_Weight;

	internal float m_EffectiveWeight;

	internal double m_EffectiveParentDelay;

	internal float m_EffectiveParentSpeed;

	internal float m_EffectiveSpeed;

	internal Flags m_Flags;

	public ulong frameId => m_FrameID;

	public float deltaTime => (float)m_DeltaTime;

	public float weight => m_Weight;

	public float effectiveWeight => m_EffectiveWeight;

	public double effectiveParentDelay => m_EffectiveParentDelay;

	public float effectiveParentSpeed => m_EffectiveParentSpeed;

	public float effectiveSpeed => m_EffectiveSpeed;

	public EvaluationType evaluationType => ((m_Flags & Flags.Evaluate) == 0) ? EvaluationType.Playback : EvaluationType.Evaluate;

	public bool seekOccurred => (m_Flags & Flags.SeekOccured) != 0;

	public bool timeLooped => (m_Flags & Flags.Loop) != 0;

	public bool timeHeld => (m_Flags & Flags.Hold) != 0;
}
