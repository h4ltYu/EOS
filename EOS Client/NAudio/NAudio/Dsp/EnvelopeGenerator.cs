using System;

namespace NAudio.Dsp
{
	/// <summary>
	/// Envelope generator (ADSR)
	/// </summary>
	// Token: 0x0200003A RID: 58
	public class EnvelopeGenerator
	{
		/// <summary>
		/// Creates and Initializes an Envelope Generator
		/// </summary>
		// Token: 0x060000F2 RID: 242 RVA: 0x000054B8 File Offset: 0x000036B8
		public EnvelopeGenerator()
		{
			this.Reset();
			this.AttackRate = 0f;
			this.DecayRate = 0f;
			this.ReleaseRate = 0f;
			this.SustainLevel = 1f;
			this.SetTargetRatioAttack(0.3f);
			this.SetTargetRatioDecayRelease(0.0001f);
		}

		/// <summary>
		/// Attack Rate (seconds * SamplesPerSecond)
		/// </summary>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00005513 File Offset: 0x00003713
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x0000551B File Offset: 0x0000371B
		public float AttackRate
		{
			get
			{
				return this.attackRate;
			}
			set
			{
				this.attackRate = value;
				this.attackCoef = EnvelopeGenerator.CalcCoef(value, this.targetRatioAttack);
				this.attackBase = (1f + this.targetRatioAttack) * (1f - this.attackCoef);
			}
		}

		/// <summary>
		/// Decay Rate (seconds * SamplesPerSecond)
		/// </summary>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00005555 File Offset: 0x00003755
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x0000555D File Offset: 0x0000375D
		public float DecayRate
		{
			get
			{
				return this.decayRate;
			}
			set
			{
				this.decayRate = value;
				this.decayCoef = EnvelopeGenerator.CalcCoef(value, this.targetRatioDecayRelease);
				this.decayBase = (this.sustainLevel - this.targetRatioDecayRelease) * (1f - this.decayCoef);
			}
		}

		/// <summary>
		/// Release Rate (seconds * SamplesPerSecond)
		/// </summary>
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00005598 File Offset: 0x00003798
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x000055A0 File Offset: 0x000037A0
		public float ReleaseRate
		{
			get
			{
				return this.releaseRate;
			}
			set
			{
				this.releaseRate = value;
				this.releaseCoef = EnvelopeGenerator.CalcCoef(value, this.targetRatioDecayRelease);
				this.releaseBase = -this.targetRatioDecayRelease * (1f - this.releaseCoef);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000055D5 File Offset: 0x000037D5
		private static float CalcCoef(float rate, float targetRatio)
		{
			return (float)Math.Exp(-Math.Log((double)((1f + targetRatio) / targetRatio)) / (double)rate);
		}

		/// <summary>
		/// Sustain Level (1 = 100%)
		/// </summary>
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000055F0 File Offset: 0x000037F0
		// (set) Token: 0x060000FB RID: 251 RVA: 0x000055F8 File Offset: 0x000037F8
		public float SustainLevel
		{
			get
			{
				return this.sustainLevel;
			}
			set
			{
				this.sustainLevel = value;
				this.decayBase = (this.sustainLevel - this.targetRatioDecayRelease) * (1f - this.decayCoef);
			}
		}

		/// <summary>
		/// Sets the attack curve
		/// </summary>
		// Token: 0x060000FC RID: 252 RVA: 0x00005621 File Offset: 0x00003821
		private void SetTargetRatioAttack(float targetRatio)
		{
			if (targetRatio < 1E-09f)
			{
				targetRatio = 1E-09f;
			}
			this.targetRatioAttack = targetRatio;
			this.attackBase = (1f + this.targetRatioAttack) * (1f - this.attackCoef);
		}

		/// <summary>
		/// Sets the decay release curve
		/// </summary>
		// Token: 0x060000FD RID: 253 RVA: 0x00005658 File Offset: 0x00003858
		private void SetTargetRatioDecayRelease(float targetRatio)
		{
			if (targetRatio < 1E-09f)
			{
				targetRatio = 1E-09f;
			}
			this.targetRatioDecayRelease = targetRatio;
			this.decayBase = (this.sustainLevel - this.targetRatioDecayRelease) * (1f - this.decayCoef);
			this.releaseBase = -this.targetRatioDecayRelease * (1f - this.releaseCoef);
		}

		/// <summary>
		/// Read the next volume multiplier from the envelope generator
		/// </summary>
		/// <returns>A volume multiplier</returns>
		// Token: 0x060000FE RID: 254 RVA: 0x000056B8 File Offset: 0x000038B8
		public float Process()
		{
			switch (this.state)
			{
			case EnvelopeGenerator.EnvelopeState.Attack:
				this.output = this.attackBase + this.output * this.attackCoef;
				if (this.output >= 1f)
				{
					this.output = 1f;
					this.state = EnvelopeGenerator.EnvelopeState.Decay;
				}
				break;
			case EnvelopeGenerator.EnvelopeState.Decay:
				this.output = this.decayBase + this.output * this.decayCoef;
				if (this.output <= this.sustainLevel)
				{
					this.output = this.sustainLevel;
					this.state = EnvelopeGenerator.EnvelopeState.Sustain;
				}
				break;
			case EnvelopeGenerator.EnvelopeState.Release:
				this.output = this.releaseBase + this.output * this.releaseCoef;
				if ((double)this.output <= 0.0)
				{
					this.output = 0f;
					this.state = EnvelopeGenerator.EnvelopeState.Idle;
				}
				break;
			}
			return this.output;
		}

		/// <summary>
		/// Trigger the gate
		/// </summary>
		/// <param name="gate">If true, enter attack phase, if false enter release phase (unless already idle)</param>
		// Token: 0x060000FF RID: 255 RVA: 0x000057AA File Offset: 0x000039AA
		public void Gate(bool gate)
		{
			if (gate)
			{
				this.state = EnvelopeGenerator.EnvelopeState.Attack;
				return;
			}
			if (this.state != EnvelopeGenerator.EnvelopeState.Idle)
			{
				this.state = EnvelopeGenerator.EnvelopeState.Release;
			}
		}

		/// <summary>
		/// Current envelope state
		/// </summary>
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000057C6 File Offset: 0x000039C6
		public EnvelopeGenerator.EnvelopeState State
		{
			get
			{
				return this.state;
			}
		}

		/// <summary>
		/// Reset to idle state
		/// </summary>
		// Token: 0x06000101 RID: 257 RVA: 0x000057CE File Offset: 0x000039CE
		public void Reset()
		{
			this.state = EnvelopeGenerator.EnvelopeState.Idle;
			this.output = 0f;
		}

		/// <summary>
		/// Get the current output level
		/// </summary>
		// Token: 0x06000102 RID: 258 RVA: 0x000057E2 File Offset: 0x000039E2
		public float GetOutput()
		{
			return this.output;
		}

		// Token: 0x040000B7 RID: 183
		private EnvelopeGenerator.EnvelopeState state;

		// Token: 0x040000B8 RID: 184
		private float output;

		// Token: 0x040000B9 RID: 185
		private float attackRate;

		// Token: 0x040000BA RID: 186
		private float decayRate;

		// Token: 0x040000BB RID: 187
		private float releaseRate;

		// Token: 0x040000BC RID: 188
		private float attackCoef;

		// Token: 0x040000BD RID: 189
		private float decayCoef;

		// Token: 0x040000BE RID: 190
		private float releaseCoef;

		// Token: 0x040000BF RID: 191
		private float sustainLevel;

		// Token: 0x040000C0 RID: 192
		private float targetRatioAttack;

		// Token: 0x040000C1 RID: 193
		private float targetRatioDecayRelease;

		// Token: 0x040000C2 RID: 194
		private float attackBase;

		// Token: 0x040000C3 RID: 195
		private float decayBase;

		// Token: 0x040000C4 RID: 196
		private float releaseBase;

		/// <summary>
		/// Envelope State
		/// </summary>
		// Token: 0x0200003B RID: 59
		public enum EnvelopeState
		{
			/// <summary>
			/// Idle
			/// </summary>
			// Token: 0x040000C6 RID: 198
			Idle,
			/// <summary>
			/// Attack
			/// </summary>
			// Token: 0x040000C7 RID: 199
			Attack,
			/// <summary>
			/// Decay
			/// </summary>
			// Token: 0x040000C8 RID: 200
			Decay,
			/// <summary>
			/// Sustain
			/// </summary>
			// Token: 0x040000C9 RID: 201
			Sustain,
			/// <summary>
			/// Release
			/// </summary>
			// Token: 0x040000CA RID: 202
			Release
		}
	}
}
