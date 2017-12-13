using System;

namespace NAudio.SoundFont
{
	/// <summary>
	/// Modulator Type
	/// </summary>
	// Token: 0x020000BD RID: 189
	public class ModulatorType
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x0000DDBC File Offset: 0x0000BFBC
		internal ModulatorType(ushort raw)
		{
			this.polarity = ((raw & 512) == 512);
			this.direction = ((raw & 256) == 256);
			this.midiContinuousController = ((raw & 128) == 128);
			this.sourceType = (SourceTypeEnum)((raw & 64512) >> 10);
			this.controllerSource = (ControllerSourceEnum)(raw & 127);
			this.midiContinuousControllerNumber = (raw & 127);
		}

		/// <summary>
		/// <see cref="M:System.Object.ToString" />
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000427 RID: 1063 RVA: 0x0000DE30 File Offset: 0x0000C030
		public override string ToString()
		{
			if (this.midiContinuousController)
			{
				return string.Format("{0} CC{1}", this.sourceType, this.midiContinuousControllerNumber);
			}
			return string.Format("{0} {1}", this.sourceType, this.controllerSource);
		}

		// Token: 0x040004FA RID: 1274
		private bool polarity;

		// Token: 0x040004FB RID: 1275
		private bool direction;

		// Token: 0x040004FC RID: 1276
		private bool midiContinuousController;

		// Token: 0x040004FD RID: 1277
		private ControllerSourceEnum controllerSource;

		// Token: 0x040004FE RID: 1278
		private SourceTypeEnum sourceType;

		// Token: 0x040004FF RID: 1279
		private ushort midiContinuousControllerNumber;
	}
}
