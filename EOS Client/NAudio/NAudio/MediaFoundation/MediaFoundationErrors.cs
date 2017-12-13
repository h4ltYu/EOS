using System;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Media Foundation Errors
	///
	/// </summary>
	/// <remarks>
	///  RANGES
	///  14000 - 14999 = General Media Foundation errors
	///  15000 - 15999 = ASF parsing errors
	///  16000 - 16999 = Media Source errors
	///  17000 - 17999 = MEDIAFOUNDATION Network Error Events
	///  18000 - 18999 = MEDIAFOUNDATION WMContainer Error Events
	///  19000 - 19999 = MEDIAFOUNDATION Media Sink Error Events
	///  20000 - 20999 = Renderer errors
	///  21000 - 21999 = Topology Errors
	///  25000 - 25999 = Timeline Errors
	///  26000 - 26999 = Unused
	///  28000 - 28999 = Transform errors
	///  29000 - 29999 = Content Protection errors
	///  40000 - 40999 = Clock errors
	///  41000 - 41999 = MF Quality Management Errors
	///  42000 - 42999 = MF Transcode API Errors
	/// </remarks>
	// Token: 0x02000042 RID: 66
	public static class MediaFoundationErrors
	{
		/// MessageId: MF_E_PLATFORM_NOT_INITIALIZED
		///
		/// MessageText:
		///
		/// Platform not initialized. Please call MFStartup().%0
		// Token: 0x04000109 RID: 265
		public const int MF_E_PLATFORM_NOT_INITIALIZED = -1072875856;

		/// MessageId: MF_E_BUFFERTOOSMALL
		///
		/// MessageText:
		///
		/// The buffer was too small to carry out the requested action.%0
		// Token: 0x0400010A RID: 266
		public const int MF_E_BUFFERTOOSMALL = -1072875855;

		/// MessageId: MF_E_INVALIDREQUEST
		///
		/// MessageText:
		///
		/// The request is invalid in the current state.%0
		// Token: 0x0400010B RID: 267
		public const int MF_E_INVALIDREQUEST = -1072875854;

		/// MessageId: MF_E_INVALIDSTREAMNUMBER
		///
		/// MessageText:
		///
		/// The stream number provided was invalid.%0
		// Token: 0x0400010C RID: 268
		public const int MF_E_INVALIDSTREAMNUMBER = -1072875853;

		/// MessageId: MF_E_INVALIDMEDIATYPE
		///
		/// MessageText:
		///
		/// The data specified for the media type is invalid, inconsistent, or not supported by this object.%0
		// Token: 0x0400010D RID: 269
		public const int MF_E_INVALIDMEDIATYPE = -1072875852;

		/// MessageId: MF_E_NOTACCEPTING
		///
		/// MessageText:
		///
		/// The callee is currently not accepting further input.%0
		// Token: 0x0400010E RID: 270
		public const int MF_E_NOTACCEPTING = -1072875851;

		/// MessageId: MF_E_NOT_INITIALIZED
		///
		/// MessageText:
		///
		/// This object needs to be initialized before the requested operation can be carried out.%0
		// Token: 0x0400010F RID: 271
		public const int MF_E_NOT_INITIALIZED = -1072875850;

		/// MessageId: MF_E_UNSUPPORTED_REPRESENTATION
		///
		/// MessageText:
		///
		/// The requested representation is not supported by this object.%0
		// Token: 0x04000110 RID: 272
		public const int MF_E_UNSUPPORTED_REPRESENTATION = -1072875849;

		/// MessageId: MF_E_NO_MORE_TYPES
		///
		/// MessageText:
		///
		/// An object ran out of media types to suggest therefore the requested chain of streaming objects cannot be completed.%0
		// Token: 0x04000111 RID: 273
		public const int MF_E_NO_MORE_TYPES = -1072875847;

		/// MessageId: MF_E_UNSUPPORTED_SERVICE
		///
		/// MessageText:
		///
		/// The object does not support the specified service.%0
		// Token: 0x04000112 RID: 274
		public const int MF_E_UNSUPPORTED_SERVICE = -1072875846;

		/// MessageId: MF_E_UNEXPECTED
		///
		/// MessageText:
		///
		/// An unexpected error has occurred in the operation requested.%0
		// Token: 0x04000113 RID: 275
		public const int MF_E_UNEXPECTED = -1072875845;

		/// MessageId: MF_E_INVALIDNAME
		///
		/// MessageText:
		///
		/// Invalid name.%0
		// Token: 0x04000114 RID: 276
		public const int MF_E_INVALIDNAME = -1072875844;

		/// MessageId: MF_E_INVALIDTYPE
		///
		/// MessageText:
		///
		/// Invalid type.%0
		// Token: 0x04000115 RID: 277
		public const int MF_E_INVALIDTYPE = -1072875843;

		/// MessageId: MF_E_INVALID_FILE_FORMAT
		///
		/// MessageText:
		///
		/// The file does not conform to the relevant file format specification.
		// Token: 0x04000116 RID: 278
		public const int MF_E_INVALID_FILE_FORMAT = -1072875842;

		/// MessageId: MF_E_INVALIDINDEX
		///
		/// MessageText:
		///
		/// Invalid index.%0
		// Token: 0x04000117 RID: 279
		public const int MF_E_INVALIDINDEX = -1072875841;

		/// MessageId: MF_E_INVALID_TIMESTAMP
		///
		/// MessageText:
		///
		/// An invalid timestamp was given.%0
		// Token: 0x04000118 RID: 280
		public const int MF_E_INVALID_TIMESTAMP = -1072875840;

		/// MessageId: MF_E_UNSUPPORTED_SCHEME
		///
		/// MessageText:
		///
		/// The scheme of the given URL is unsupported.%0
		// Token: 0x04000119 RID: 281
		public const int MF_E_UNSUPPORTED_SCHEME = -1072875837;

		/// MessageId: MF_E_UNSUPPORTED_BYTESTREAM_TYPE
		///
		/// MessageText:
		///
		/// The byte stream type of the given URL is unsupported.%0
		// Token: 0x0400011A RID: 282
		public const int MF_E_UNSUPPORTED_BYTESTREAM_TYPE = -1072875836;

		/// MessageId: MF_E_UNSUPPORTED_TIME_FORMAT
		///
		/// MessageText:
		///
		/// The given time format is unsupported.%0
		// Token: 0x0400011B RID: 283
		public const int MF_E_UNSUPPORTED_TIME_FORMAT = -1072875835;

		/// MessageId: MF_E_NO_SAMPLE_TIMESTAMP
		///
		/// MessageText:
		///
		/// The Media Sample does not have a timestamp.%0
		// Token: 0x0400011C RID: 284
		public const int MF_E_NO_SAMPLE_TIMESTAMP = -1072875832;

		/// MessageId: MF_E_NO_SAMPLE_DURATION
		///
		/// MessageText:
		///
		/// The Media Sample does not have a duration.%0
		// Token: 0x0400011D RID: 285
		public const int MF_E_NO_SAMPLE_DURATION = -1072875831;

		/// MessageId: MF_E_INVALID_STREAM_DATA
		///
		/// MessageText:
		///
		/// The request failed because the data in the stream is corrupt.%0\n.
		// Token: 0x0400011E RID: 286
		public const int MF_E_INVALID_STREAM_DATA = -1072875829;

		/// MessageId: MF_E_RT_UNAVAILABLE
		///
		/// MessageText:
		///
		/// Real time services are not available.%0
		// Token: 0x0400011F RID: 287
		public const int MF_E_RT_UNAVAILABLE = -1072875825;

		/// MessageId: MF_E_UNSUPPORTED_RATE
		///
		/// MessageText:
		///
		/// The specified rate is not supported.%0
		// Token: 0x04000120 RID: 288
		public const int MF_E_UNSUPPORTED_RATE = -1072875824;

		/// MessageId: MF_E_THINNING_UNSUPPORTED
		///
		/// MessageText:
		///
		/// This component does not support stream-thinning.%0
		// Token: 0x04000121 RID: 289
		public const int MF_E_THINNING_UNSUPPORTED = -1072875823;

		/// MessageId: MF_E_REVERSE_UNSUPPORTED
		///
		/// MessageText:
		///
		/// The call failed because no reverse playback rates are available.%0
		// Token: 0x04000122 RID: 290
		public const int MF_E_REVERSE_UNSUPPORTED = -1072875822;

		/// MessageId: MF_E_UNSUPPORTED_RATE_TRANSITION
		///
		/// MessageText:
		///
		/// The requested rate transition cannot occur in the current state.%0
		// Token: 0x04000123 RID: 291
		public const int MF_E_UNSUPPORTED_RATE_TRANSITION = -1072875821;

		/// MessageId: MF_E_RATE_CHANGE_PREEMPTED
		///
		/// MessageText:
		///
		/// The requested rate change has been pre-empted and will not occur.%0
		// Token: 0x04000124 RID: 292
		public const int MF_E_RATE_CHANGE_PREEMPTED = -1072875820;

		/// MessageId: MF_E_NOT_FOUND
		///
		/// MessageText:
		///
		/// The specified object or value does not exist.%0
		// Token: 0x04000125 RID: 293
		public const int MF_E_NOT_FOUND = -1072875819;

		/// MessageId: MF_E_NOT_AVAILABLE
		///
		/// MessageText:
		///
		/// The requested value is not available.%0
		// Token: 0x04000126 RID: 294
		public const int MF_E_NOT_AVAILABLE = -1072875818;

		/// MessageId: MF_E_NO_CLOCK
		///
		/// MessageText:
		///
		/// The specified operation requires a clock and no clock is available.%0
		// Token: 0x04000127 RID: 295
		public const int MF_E_NO_CLOCK = -1072875817;

		/// MessageId: MF_S_MULTIPLE_BEGIN
		///
		/// MessageText:
		///
		/// This callback and state had already been passed in to this event generator earlier.%0
		// Token: 0x04000128 RID: 296
		public const int MF_S_MULTIPLE_BEGIN = 866008;

		/// MessageId: MF_E_MULTIPLE_BEGIN
		///
		/// MessageText:
		///
		/// This callback has already been passed in to this event generator.%0
		// Token: 0x04000129 RID: 297
		public const int MF_E_MULTIPLE_BEGIN = -1072875815;

		/// MessageId: MF_E_MULTIPLE_SUBSCRIBERS
		///
		/// MessageText:
		///
		/// Some component is already listening to events on this event generator.%0
		// Token: 0x0400012A RID: 298
		public const int MF_E_MULTIPLE_SUBSCRIBERS = -1072875814;

		/// MessageId: MF_E_TIMER_ORPHANED
		///
		/// MessageText:
		///
		/// This timer was orphaned before its callback time arrived.%0
		// Token: 0x0400012B RID: 299
		public const int MF_E_TIMER_ORPHANED = -1072875813;

		/// MessageId: MF_E_STATE_TRANSITION_PENDING
		///
		/// MessageText:
		///
		/// A state transition is already pending.%0
		// Token: 0x0400012C RID: 300
		public const int MF_E_STATE_TRANSITION_PENDING = -1072875812;

		/// MessageId: MF_E_UNSUPPORTED_STATE_TRANSITION
		///
		/// MessageText:
		///
		/// The requested state transition is unsupported.%0
		// Token: 0x0400012D RID: 301
		public const int MF_E_UNSUPPORTED_STATE_TRANSITION = -1072875811;

		/// MessageId: MF_E_UNRECOVERABLE_ERROR_OCCURRED
		///
		/// MessageText:
		///
		/// An unrecoverable error has occurred.%0
		// Token: 0x0400012E RID: 302
		public const int MF_E_UNRECOVERABLE_ERROR_OCCURRED = -1072875810;

		/// MessageId: MF_E_SAMPLE_HAS_TOO_MANY_BUFFERS
		///
		/// MessageText:
		///
		/// The provided sample has too many buffers.%0
		// Token: 0x0400012F RID: 303
		public const int MF_E_SAMPLE_HAS_TOO_MANY_BUFFERS = -1072875809;

		/// MessageId: MF_E_SAMPLE_NOT_WRITABLE
		///
		/// MessageText:
		///
		/// The provided sample is not writable.%0
		// Token: 0x04000130 RID: 304
		public const int MF_E_SAMPLE_NOT_WRITABLE = -1072875808;

		/// MessageId: MF_E_INVALID_KEY
		///
		/// MessageText:
		///
		/// The specified key is not valid.
		// Token: 0x04000131 RID: 305
		public const int MF_E_INVALID_KEY = -1072875806;

		/// MessageId: MF_E_BAD_STARTUP_VERSION
		///
		/// MessageText:
		///
		/// You are calling MFStartup with the wrong MF_VERSION. Mismatched bits?
		// Token: 0x04000132 RID: 306
		public const int MF_E_BAD_STARTUP_VERSION = -1072875805;

		/// MessageId: MF_E_UNSUPPORTED_CAPTION
		///
		/// MessageText:
		///
		/// The caption of the given URL is unsupported.%0
		// Token: 0x04000133 RID: 307
		public const int MF_E_UNSUPPORTED_CAPTION = -1072875804;

		/// MessageId: MF_E_INVALID_POSITION
		///
		/// MessageText:
		///
		/// The operation on the current offset is not permitted.%0
		// Token: 0x04000134 RID: 308
		public const int MF_E_INVALID_POSITION = -1072875803;

		/// MessageId: MF_E_ATTRIBUTENOTFOUND
		///
		/// MessageText:
		///
		/// The requested attribute was not found.%0
		// Token: 0x04000135 RID: 309
		public const int MF_E_ATTRIBUTENOTFOUND = -1072875802;

		/// MessageId: MF_E_PROPERTY_TYPE_NOT_ALLOWED
		///
		/// MessageText:
		///
		/// The specified property type is not allowed in this context.%0
		// Token: 0x04000136 RID: 310
		public const int MF_E_PROPERTY_TYPE_NOT_ALLOWED = -1072875801;

		/// MessageId: MF_E_PROPERTY_TYPE_NOT_SUPPORTED
		///
		/// MessageText:
		///
		/// The specified property type is not supported.%0
		// Token: 0x04000137 RID: 311
		public const int MF_E_PROPERTY_TYPE_NOT_SUPPORTED = -1072875800;

		/// MessageId: MF_E_PROPERTY_EMPTY
		///
		/// MessageText:
		///
		/// The specified property is empty.%0
		// Token: 0x04000138 RID: 312
		public const int MF_E_PROPERTY_EMPTY = -1072875799;

		/// MessageId: MF_E_PROPERTY_NOT_EMPTY
		///
		/// MessageText:
		///
		/// The specified property is not empty.%0
		// Token: 0x04000139 RID: 313
		public const int MF_E_PROPERTY_NOT_EMPTY = -1072875798;

		/// MessageId: MF_E_PROPERTY_VECTOR_NOT_ALLOWED
		///
		/// MessageText:
		///
		/// The vector property specified is not allowed in this context.%0
		// Token: 0x0400013A RID: 314
		public const int MF_E_PROPERTY_VECTOR_NOT_ALLOWED = -1072875797;

		/// MessageId: MF_E_PROPERTY_VECTOR_REQUIRED
		///
		/// MessageText:
		///
		/// A vector property is required in this context.%0
		// Token: 0x0400013B RID: 315
		public const int MF_E_PROPERTY_VECTOR_REQUIRED = -1072875796;

		/// MessageId: MF_E_OPERATION_CANCELLED
		///
		/// MessageText:
		///
		/// The operation is cancelled.%0
		// Token: 0x0400013C RID: 316
		public const int MF_E_OPERATION_CANCELLED = -1072875795;

		/// MessageId: MF_E_BYTESTREAM_NOT_SEEKABLE
		///
		/// MessageText:
		///
		/// The provided bytestream was expected to be seekable and it is not.%0
		// Token: 0x0400013D RID: 317
		public const int MF_E_BYTESTREAM_NOT_SEEKABLE = -1072875794;

		/// MessageId: MF_E_DISABLED_IN_SAFEMODE
		///
		/// MessageText:
		///
		/// The Media Foundation platform is disabled when the system is running in Safe Mode.%0
		// Token: 0x0400013E RID: 318
		public const int MF_E_DISABLED_IN_SAFEMODE = -1072875793;

		/// MessageId: MF_E_CANNOT_PARSE_BYTESTREAM
		///
		/// MessageText:
		///
		/// The Media Source could not parse the byte stream.%0
		// Token: 0x0400013F RID: 319
		public const int MF_E_CANNOT_PARSE_BYTESTREAM = -1072875792;

		/// MessageId: MF_E_SOURCERESOLVER_MUTUALLY_EXCLUSIVE_FLAGS
		///
		/// MessageText:
		///
		/// Mutually exclusive flags have been specified to source resolver. This flag combination is invalid.%0
		// Token: 0x04000140 RID: 320
		public const int MF_E_SOURCERESOLVER_MUTUALLY_EXCLUSIVE_FLAGS = -1072875791;

		/// MessageId: MF_E_MEDIAPROC_WRONGSTATE
		///
		/// MessageText:
		///
		/// MediaProc is in the wrong state%0
		// Token: 0x04000141 RID: 321
		public const int MF_E_MEDIAPROC_WRONGSTATE = -1072875790;

		/// MessageId: MF_E_RT_THROUGHPUT_NOT_AVAILABLE
		///
		/// MessageText:
		///
		/// Real time I/O service can not provide requested throughput.%0
		// Token: 0x04000142 RID: 322
		public const int MF_E_RT_THROUGHPUT_NOT_AVAILABLE = -1072875789;

		/// MessageId: MF_E_RT_TOO_MANY_CLASSES
		///
		/// MessageText:
		///
		/// The workqueue cannot be registered with more classes.%0
		// Token: 0x04000143 RID: 323
		public const int MF_E_RT_TOO_MANY_CLASSES = -1072875788;

		/// MessageId: MF_E_RT_WOULDBLOCK
		///
		/// MessageText:
		///
		/// This operation cannot succeed because another thread owns this object.%0
		// Token: 0x04000144 RID: 324
		public const int MF_E_RT_WOULDBLOCK = -1072875787;

		/// MessageId: MF_E_NO_BITPUMP
		///
		/// MessageText:
		///
		/// Internal. Bitpump not found.%0
		// Token: 0x04000145 RID: 325
		public const int MF_E_NO_BITPUMP = -1072875786;

		/// MessageId: MF_E_RT_OUTOFMEMORY
		///
		/// MessageText:
		///
		/// No more RT memory available.%0
		// Token: 0x04000146 RID: 326
		public const int MF_E_RT_OUTOFMEMORY = -1072875785;

		/// MessageId: MF_E_RT_WORKQUEUE_CLASS_NOT_SPECIFIED
		///
		/// MessageText:
		///
		/// An MMCSS class has not been set for this work queue.%0
		// Token: 0x04000147 RID: 327
		public const int MF_E_RT_WORKQUEUE_CLASS_NOT_SPECIFIED = -1072875784;

		/// MessageId: MF_E_INSUFFICIENT_BUFFER
		///
		/// MessageText:
		///
		/// Insufficient memory for response.%0
		// Token: 0x04000148 RID: 328
		public const int MF_E_INSUFFICIENT_BUFFER = -1072860816;

		/// MessageId: MF_E_CANNOT_CREATE_SINK
		///
		/// MessageText:
		///
		/// Activate failed to create mediasink. Call OutputNode::GetUINT32(MF_TOPONODE_MAJORTYPE) for more information. %0
		// Token: 0x04000149 RID: 329
		public const int MF_E_CANNOT_CREATE_SINK = -1072875782;

		/// MessageId: MF_E_BYTESTREAM_UNKNOWN_LENGTH
		///
		/// MessageText:
		///
		/// The length of the provided bytestream is unknown.%0
		// Token: 0x0400014A RID: 330
		public const int MF_E_BYTESTREAM_UNKNOWN_LENGTH = -1072875781;

		/// MessageId: MF_E_SESSION_PAUSEWHILESTOPPED
		///
		/// MessageText:
		///
		/// The media session cannot pause from a stopped state.%0
		// Token: 0x0400014B RID: 331
		public const int MF_E_SESSION_PAUSEWHILESTOPPED = -1072875780;

		/// MessageId: MF_S_ACTIVATE_REPLACED
		///
		/// MessageText:
		///
		/// The activate could not be created in the remote process for some reason it was replaced with empty one.%0
		// Token: 0x0400014C RID: 332
		public const int MF_S_ACTIVATE_REPLACED = 866045;

		/// MessageId: MF_E_FORMAT_CHANGE_NOT_SUPPORTED
		///
		/// MessageText:
		///
		/// The data specified for the media type is supported, but would require a format change, which is not supported by this object.%0
		// Token: 0x0400014D RID: 333
		public const int MF_E_FORMAT_CHANGE_NOT_SUPPORTED = -1072875778;

		/// MessageId: MF_E_INVALID_WORKQUEUE
		///
		/// MessageText:
		///
		/// The operation failed because an invalid combination of workqueue ID and flags was specified.%0
		// Token: 0x0400014E RID: 334
		public const int MF_E_INVALID_WORKQUEUE = -1072875777;

		/// MessageId: MF_E_DRM_UNSUPPORTED
		///
		/// MessageText:
		///
		/// No DRM support is available.%0
		// Token: 0x0400014F RID: 335
		public const int MF_E_DRM_UNSUPPORTED = -1072875776;

		/// MessageId: MF_E_UNAUTHORIZED
		///
		/// MessageText:
		///
		/// This operation is not authorized.%0
		// Token: 0x04000150 RID: 336
		public const int MF_E_UNAUTHORIZED = -1072875775;

		/// MessageId: MF_E_OUT_OF_RANGE
		///
		/// MessageText:
		///
		/// The value is not in the specified or valid range.%0
		// Token: 0x04000151 RID: 337
		public const int MF_E_OUT_OF_RANGE = -1072875774;

		/// MessageId: MF_E_INVALID_CODEC_MERIT
		///
		/// MessageText:
		///
		/// The registered codec merit is not valid.%0
		// Token: 0x04000152 RID: 338
		public const int MF_E_INVALID_CODEC_MERIT = -1072875773;

		/// MessageId: MF_E_HW_MFT_FAILED_START_STREAMING
		///
		/// MessageText:
		///
		/// Hardware MFT failed to start streaming due to lack of hardware resources.%0
		// Token: 0x04000153 RID: 339
		public const int MF_E_HW_MFT_FAILED_START_STREAMING = -1072875772;

		/// MessageId: MF_S_ASF_PARSEINPROGRESS
		///
		/// MessageText:
		///
		/// Parsing is still in progress and is not yet complete.%0
		// Token: 0x04000154 RID: 340
		public const int MF_S_ASF_PARSEINPROGRESS = 1074608792;

		/// MessageId: MF_E_ASF_PARSINGINCOMPLETE
		///
		/// MessageText:
		///
		/// Not enough data have been parsed to carry out the requested action.%0
		// Token: 0x04000155 RID: 341
		public const int MF_E_ASF_PARSINGINCOMPLETE = -1072874856;

		/// MessageId: MF_E_ASF_MISSINGDATA
		///
		/// MessageText:
		///
		/// There is a gap in the ASF data provided.%0
		// Token: 0x04000156 RID: 342
		public const int MF_E_ASF_MISSINGDATA = -1072874855;

		/// MessageId: MF_E_ASF_INVALIDDATA
		///
		/// MessageText:
		///
		/// The data provided are not valid ASF.%0
		// Token: 0x04000157 RID: 343
		public const int MF_E_ASF_INVALIDDATA = -1072874854;

		/// MessageId: MF_E_ASF_OPAQUEPACKET
		///
		/// MessageText:
		///
		/// The packet is opaque, so the requested information cannot be returned.%0
		// Token: 0x04000158 RID: 344
		public const int MF_E_ASF_OPAQUEPACKET = -1072874853;

		/// MessageId: MF_E_ASF_NOINDEX
		///
		/// MessageText:
		///
		/// The requested operation failed since there is no appropriate ASF index.%0
		// Token: 0x04000159 RID: 345
		public const int MF_E_ASF_NOINDEX = -1072874852;

		/// MessageId: MF_E_ASF_OUTOFRANGE
		///
		/// MessageText:
		///
		/// The value supplied is out of range for this operation.%0
		// Token: 0x0400015A RID: 346
		public const int MF_E_ASF_OUTOFRANGE = -1072874851;

		/// MessageId: MF_E_ASF_INDEXNOTLOADED
		///
		/// MessageText:
		///
		/// The index entry requested needs to be loaded before it can be available.%0
		// Token: 0x0400015B RID: 347
		public const int MF_E_ASF_INDEXNOTLOADED = -1072874850;

		/// MessageId: MF_E_ASF_TOO_MANY_PAYLOADS
		///
		/// MessageText:
		///
		/// The packet has reached the maximum number of payloads.%0
		// Token: 0x0400015C RID: 348
		public const int MF_E_ASF_TOO_MANY_PAYLOADS = -1072874849;

		/// MessageId: MF_E_ASF_UNSUPPORTED_STREAM_TYPE
		///
		/// MessageText:
		///
		/// Stream type is not supported.%0
		// Token: 0x0400015D RID: 349
		public const int MF_E_ASF_UNSUPPORTED_STREAM_TYPE = -1072874848;

		/// MessageId: MF_E_ASF_DROPPED_PACKET
		///
		/// MessageText:
		///
		/// One or more ASF packets were dropped.%0
		// Token: 0x0400015E RID: 350
		public const int MF_E_ASF_DROPPED_PACKET = -1072874847;

		/// MessageId: MF_E_NO_EVENTS_AVAILABLE
		///
		/// MessageText:
		///
		/// There are no events available in the queue.%0
		// Token: 0x0400015F RID: 351
		public const int MF_E_NO_EVENTS_AVAILABLE = -1072873856;

		/// MessageId: MF_E_INVALID_STATE_TRANSITION
		///
		/// MessageText:
		///
		/// A media source cannot go from the stopped state to the paused state.%0
		// Token: 0x04000160 RID: 352
		public const int MF_E_INVALID_STATE_TRANSITION = -1072873854;

		/// MessageId: MF_E_END_OF_STREAM
		///
		/// MessageText:
		///
		/// The media stream cannot process any more samples because there are no more samples in the stream.%0
		// Token: 0x04000161 RID: 353
		public const int MF_E_END_OF_STREAM = -1072873852;

		/// MessageId: MF_E_SHUTDOWN
		///
		/// MessageText:
		///
		/// The request is invalid because Shutdown() has been called.%0
		// Token: 0x04000162 RID: 354
		public const int MF_E_SHUTDOWN = -1072873851;

		/// MessageId: MF_E_MP3_NOTFOUND
		///
		/// MessageText:
		///
		/// The MP3 object was not found.%0
		// Token: 0x04000163 RID: 355
		public const int MF_E_MP3_NOTFOUND = -1072873850;

		/// MessageId: MF_E_MP3_OUTOFDATA
		///
		/// MessageText:
		///
		/// The MP3 parser ran out of data before finding the MP3 object.%0
		// Token: 0x04000164 RID: 356
		public const int MF_E_MP3_OUTOFDATA = -1072873849;

		/// MessageId: MF_E_MP3_NOTMP3
		///
		/// MessageText:
		///
		/// The file is not really a MP3 file.%0
		// Token: 0x04000165 RID: 357
		public const int MF_E_MP3_NOTMP3 = -1072873848;

		/// MessageId: MF_E_MP3_NOTSUPPORTED
		///
		/// MessageText:
		///
		/// The MP3 file is not supported.%0
		// Token: 0x04000166 RID: 358
		public const int MF_E_MP3_NOTSUPPORTED = -1072873847;

		/// MessageId: MF_E_NO_DURATION
		///
		/// MessageText:
		///
		/// The Media stream has no duration.%0
		// Token: 0x04000167 RID: 359
		public const int MF_E_NO_DURATION = -1072873846;

		/// MessageId: MF_E_INVALID_FORMAT
		///
		/// MessageText:
		///
		/// The Media format is recognized but is invalid.%0
		// Token: 0x04000168 RID: 360
		public const int MF_E_INVALID_FORMAT = -1072873844;

		/// MessageId: MF_E_PROPERTY_NOT_FOUND
		///
		/// MessageText:
		///
		/// The property requested was not found.%0
		// Token: 0x04000169 RID: 361
		public const int MF_E_PROPERTY_NOT_FOUND = -1072873843;

		/// MessageId: MF_E_PROPERTY_READ_ONLY
		///
		/// MessageText:
		///
		/// The property is read only.%0
		// Token: 0x0400016A RID: 362
		public const int MF_E_PROPERTY_READ_ONLY = -1072873842;

		/// MessageId: MF_E_PROPERTY_NOT_ALLOWED
		///
		/// MessageText:
		///
		/// The specified property is not allowed in this context.%0
		// Token: 0x0400016B RID: 363
		public const int MF_E_PROPERTY_NOT_ALLOWED = -1072873841;

		/// MessageId: MF_E_MEDIA_SOURCE_NOT_STARTED
		///
		/// MessageText:
		///
		/// The media source is not started.%0
		// Token: 0x0400016C RID: 364
		public const int MF_E_MEDIA_SOURCE_NOT_STARTED = -1072873839;

		/// MessageId: MF_E_UNSUPPORTED_FORMAT
		///
		/// MessageText:
		///
		/// The Media format is recognized but not supported.%0
		// Token: 0x0400016D RID: 365
		public const int MF_E_UNSUPPORTED_FORMAT = -1072873832;

		/// MessageId: MF_E_MP3_BAD_CRC
		///
		/// MessageText:
		///
		/// The MPEG frame has bad CRC.%0
		// Token: 0x0400016E RID: 366
		public const int MF_E_MP3_BAD_CRC = -1072873831;

		/// MessageId: MF_E_NOT_PROTECTED
		///
		/// MessageText:
		///
		/// The file is not protected.%0
		// Token: 0x0400016F RID: 367
		public const int MF_E_NOT_PROTECTED = -1072873830;

		/// MessageId: MF_E_MEDIA_SOURCE_WRONGSTATE
		///
		/// MessageText:
		///
		/// The media source is in the wrong state%0
		// Token: 0x04000170 RID: 368
		public const int MF_E_MEDIA_SOURCE_WRONGSTATE = -1072873829;

		/// MessageId: MF_E_MEDIA_SOURCE_NO_STREAMS_SELECTED
		///
		/// MessageText:
		///
		/// No streams are selected in source presentation descriptor.%0
		// Token: 0x04000171 RID: 369
		public const int MF_E_MEDIA_SOURCE_NO_STREAMS_SELECTED = -1072873828;

		/// MessageId: MF_E_CANNOT_FIND_KEYFRAME_SAMPLE
		///
		/// MessageText:
		///
		/// No key frame sample was found.%0
		// Token: 0x04000172 RID: 370
		public const int MF_E_CANNOT_FIND_KEYFRAME_SAMPLE = -1072873827;

		/// MessageId: MF_E_NETWORK_RESOURCE_FAILURE
		///
		/// MessageText:
		///
		/// An attempt to acquire a network resource failed.%0
		// Token: 0x04000173 RID: 371
		public const int MF_E_NETWORK_RESOURCE_FAILURE = -1072872856;

		/// MessageId: MF_E_NET_WRITE
		///
		/// MessageText:
		///
		/// Error writing to the network.%0
		// Token: 0x04000174 RID: 372
		public const int MF_E_NET_WRITE = -1072872855;

		/// MessageId: MF_E_NET_READ
		///
		/// MessageText:
		///
		/// Error reading from the network.%0
		// Token: 0x04000175 RID: 373
		public const int MF_E_NET_READ = -1072872854;

		/// MessageId: MF_E_NET_REQUIRE_NETWORK
		///
		/// MessageText:
		///
		/// Internal. Entry cannot complete operation without network.%0
		// Token: 0x04000176 RID: 374
		public const int MF_E_NET_REQUIRE_NETWORK = -1072872853;

		/// MessageId: MF_E_NET_REQUIRE_ASYNC
		///
		/// MessageText:
		///
		/// Internal. Async op is required.%0
		// Token: 0x04000177 RID: 375
		public const int MF_E_NET_REQUIRE_ASYNC = -1072872852;

		/// MessageId: MF_E_NET_BWLEVEL_NOT_SUPPORTED
		///
		/// MessageText:
		///
		/// Internal. Bandwidth levels are not supported.%0
		// Token: 0x04000178 RID: 376
		public const int MF_E_NET_BWLEVEL_NOT_SUPPORTED = -1072872851;

		/// MessageId: MF_E_NET_STREAMGROUPS_NOT_SUPPORTED
		///
		/// MessageText:
		///
		/// Internal. Stream groups are not supported.%0
		// Token: 0x04000179 RID: 377
		public const int MF_E_NET_STREAMGROUPS_NOT_SUPPORTED = -1072872850;

		/// MessageId: MF_E_NET_MANUALSS_NOT_SUPPORTED
		///
		/// MessageText:
		///
		/// Manual stream selection is not supported.%0
		// Token: 0x0400017A RID: 378
		public const int MF_E_NET_MANUALSS_NOT_SUPPORTED = -1072872849;

		/// MessageId: MF_E_NET_INVALID_PRESENTATION_DESCRIPTOR
		///
		/// MessageText:
		///
		/// Invalid presentation descriptor.%0
		// Token: 0x0400017B RID: 379
		public const int MF_E_NET_INVALID_PRESENTATION_DESCRIPTOR = -1072872848;

		/// MessageId: MF_E_NET_CACHESTREAM_NOT_FOUND
		///
		/// MessageText:
		///
		/// Cannot find cache stream.%0
		// Token: 0x0400017C RID: 380
		public const int MF_E_NET_CACHESTREAM_NOT_FOUND = -1072872847;

		/// MessageId: MF_I_MANUAL_PROXY
		///
		/// MessageText:
		///
		/// The proxy setting is manual.%0
		// Token: 0x0400017D RID: 381
		public const int MF_I_MANUAL_PROXY = 1074610802;

		/// duplicate removed
		/// MessageId=17011 Severity=Informational Facility=MEDIAFOUNDATION SymbolicName=MF_E_INVALID_REQUEST
		/// Language=English
		/// The request is invalid in the current state.%0
		/// .
		///
		///  MessageId: MF_E_NET_REQUIRE_INPUT
		///
		///  MessageText:
		///
		///  Internal. Entry cannot complete operation without input.%0
		// Token: 0x0400017E RID: 382
		public const int MF_E_NET_REQUIRE_INPUT = -1072872844;

		/// MessageId: MF_E_NET_REDIRECT
		///
		/// MessageText:
		///
		/// The client redirected to another server.%0
		// Token: 0x0400017F RID: 383
		public const int MF_E_NET_REDIRECT = -1072872843;

		/// MessageId: MF_E_NET_REDIRECT_TO_PROXY
		///
		/// MessageText:
		///
		/// The client is redirected to a proxy server.%0
		// Token: 0x04000180 RID: 384
		public const int MF_E_NET_REDIRECT_TO_PROXY = -1072872842;

		/// MessageId: MF_E_NET_TOO_MANY_REDIRECTS
		///
		/// MessageText:
		///
		/// The client reached maximum redirection limit.%0
		// Token: 0x04000181 RID: 385
		public const int MF_E_NET_TOO_MANY_REDIRECTS = -1072872841;

		/// MessageId: MF_E_NET_TIMEOUT
		///
		/// MessageText:
		///
		/// The server, a computer set up to offer multimedia content to other computers, could not handle your request for multimedia content in a timely manner.  Please try again later.%0
		// Token: 0x04000182 RID: 386
		public const int MF_E_NET_TIMEOUT = -1072872840;

		/// MessageId: MF_E_NET_CLIENT_CLOSE
		///
		/// MessageText:
		///
		/// The control socket is closed by the client.%0
		// Token: 0x04000183 RID: 387
		public const int MF_E_NET_CLIENT_CLOSE = -1072872839;

		/// MessageId: MF_E_NET_BAD_CONTROL_DATA
		///
		/// MessageText:
		///
		/// The server received invalid data from the client on the control connection.%0
		// Token: 0x04000184 RID: 388
		public const int MF_E_NET_BAD_CONTROL_DATA = -1072872838;

		/// MessageId: MF_E_NET_INCOMPATIBLE_SERVER
		///
		/// MessageText:
		///
		/// The server is not a compatible streaming media server.%0
		// Token: 0x04000185 RID: 389
		public const int MF_E_NET_INCOMPATIBLE_SERVER = -1072872837;

		/// MessageId: MF_E_NET_UNSAFE_URL
		///
		/// MessageText:
		///
		/// Url.%0
		// Token: 0x04000186 RID: 390
		public const int MF_E_NET_UNSAFE_URL = -1072872836;

		/// MessageId: MF_E_NET_CACHE_NO_DATA
		///
		/// MessageText:
		///
		/// Data is not available.%0
		// Token: 0x04000187 RID: 391
		public const int MF_E_NET_CACHE_NO_DATA = -1072872835;

		/// MessageId: MF_E_NET_EOL
		///
		/// MessageText:
		///
		/// End of line.%0
		// Token: 0x04000188 RID: 392
		public const int MF_E_NET_EOL = -1072872834;

		/// MessageId: MF_E_NET_BAD_REQUEST
		///
		/// MessageText:
		///
		/// The request could not be understood by the server.%0
		// Token: 0x04000189 RID: 393
		public const int MF_E_NET_BAD_REQUEST = -1072872833;

		/// MessageId: MF_E_NET_INTERNAL_SERVER_ERROR
		///
		/// MessageText:
		///
		/// The server encountered an unexpected condition which prevented it from fulfilling the request.%0
		// Token: 0x0400018A RID: 394
		public const int MF_E_NET_INTERNAL_SERVER_ERROR = -1072872832;

		/// MessageId: MF_E_NET_SESSION_NOT_FOUND
		///
		/// MessageText:
		///
		/// Session not found.%0
		// Token: 0x0400018B RID: 395
		public const int MF_E_NET_SESSION_NOT_FOUND = -1072872831;

		/// MessageId: MF_E_NET_NOCONNECTION
		///
		/// MessageText:
		///
		/// There is no connection established with the Windows Media server. The operation failed.%0
		// Token: 0x0400018C RID: 396
		public const int MF_E_NET_NOCONNECTION = -1072872830;

		/// MessageId: MF_E_NET_CONNECTION_FAILURE
		///
		/// MessageText:
		///
		/// The network connection has failed.%0
		// Token: 0x0400018D RID: 397
		public const int MF_E_NET_CONNECTION_FAILURE = -1072872829;

		/// MessageId: MF_E_NET_INCOMPATIBLE_PUSHSERVER
		///
		/// MessageText:
		///
		/// The Server service that received the HTTP push request is not a compatible version of Windows Media Services (WMS).  This error may indicate the push request was received by IIS instead of WMS.  Ensure WMS is started and has the HTTP Server control protocol properly enabled and try again.%0
		// Token: 0x0400018E RID: 398
		public const int MF_E_NET_INCOMPATIBLE_PUSHSERVER = -1072872828;

		/// MessageId: MF_E_NET_SERVER_ACCESSDENIED
		///
		/// MessageText:
		///
		/// The Windows Media server is denying access.  The username and/or password might be incorrect.%0
		// Token: 0x0400018F RID: 399
		public const int MF_E_NET_SERVER_ACCESSDENIED = -1072872827;

		/// MessageId: MF_E_NET_PROXY_ACCESSDENIED
		///
		/// MessageText:
		///
		/// The proxy server is denying access.  The username and/or password might be incorrect.%0
		// Token: 0x04000190 RID: 400
		public const int MF_E_NET_PROXY_ACCESSDENIED = -1072872826;

		/// MessageId: MF_E_NET_CANNOTCONNECT
		///
		/// MessageText:
		///
		/// Unable to establish a connection to the server.%0
		// Token: 0x04000191 RID: 401
		public const int MF_E_NET_CANNOTCONNECT = -1072872825;

		/// MessageId: MF_E_NET_INVALID_PUSH_TEMPLATE
		///
		/// MessageText:
		///
		/// The specified push template is invalid.%0
		// Token: 0x04000192 RID: 402
		public const int MF_E_NET_INVALID_PUSH_TEMPLATE = -1072872824;

		/// MessageId: MF_E_NET_INVALID_PUSH_PUBLISHING_POINT
		///
		/// MessageText:
		///
		/// The specified push publishing point is invalid.%0
		// Token: 0x04000193 RID: 403
		public const int MF_E_NET_INVALID_PUSH_PUBLISHING_POINT = -1072872823;

		/// MessageId: MF_E_NET_BUSY
		///
		/// MessageText:
		///
		/// The requested resource is in use.%0
		// Token: 0x04000194 RID: 404
		public const int MF_E_NET_BUSY = -1072872822;

		/// MessageId: MF_E_NET_RESOURCE_GONE
		///
		/// MessageText:
		///
		/// The Publishing Point or file on the Windows Media Server is no longer available.%0
		// Token: 0x04000195 RID: 405
		public const int MF_E_NET_RESOURCE_GONE = -1072872821;

		/// MessageId: MF_E_NET_ERROR_FROM_PROXY
		///
		/// MessageText:
		///
		/// The proxy experienced an error while attempting to contact the media server.%0
		// Token: 0x04000196 RID: 406
		public const int MF_E_NET_ERROR_FROM_PROXY = -1072872820;

		/// MessageId: MF_E_NET_PROXY_TIMEOUT
		///
		/// MessageText:
		///
		/// The proxy did not receive a timely response while attempting to contact the media server.%0
		// Token: 0x04000197 RID: 407
		public const int MF_E_NET_PROXY_TIMEOUT = -1072872819;

		/// MessageId: MF_E_NET_SERVER_UNAVAILABLE
		///
		/// MessageText:
		///
		/// The server is currently unable to handle the request due to a temporary overloading or maintenance of the server.%0
		// Token: 0x04000198 RID: 408
		public const int MF_E_NET_SERVER_UNAVAILABLE = -1072872818;

		/// MessageId: MF_E_NET_TOO_MUCH_DATA
		///
		/// MessageText:
		///
		/// The encoding process was unable to keep up with the amount of supplied data.%0
		// Token: 0x04000199 RID: 409
		public const int MF_E_NET_TOO_MUCH_DATA = -1072872817;

		/// MessageId: MF_E_NET_SESSION_INVALID
		///
		/// MessageText:
		///
		/// Session not found.%0
		// Token: 0x0400019A RID: 410
		public const int MF_E_NET_SESSION_INVALID = -1072872816;

		/// MessageId: MF_E_OFFLINE_MODE
		///
		/// MessageText:
		///
		/// The requested URL is not available in offline mode.%0
		// Token: 0x0400019B RID: 411
		public const int MF_E_OFFLINE_MODE = -1072872815;

		/// MessageId: MF_E_NET_UDP_BLOCKED
		///
		/// MessageText:
		///
		/// A device in the network is blocking UDP traffic.%0
		// Token: 0x0400019C RID: 412
		public const int MF_E_NET_UDP_BLOCKED = -1072872814;

		/// MessageId: MF_E_NET_UNSUPPORTED_CONFIGURATION
		///
		/// MessageText:
		///
		/// The specified configuration value is not supported.%0
		// Token: 0x0400019D RID: 413
		public const int MF_E_NET_UNSUPPORTED_CONFIGURATION = -1072872813;

		/// MessageId: MF_E_NET_PROTOCOL_DISABLED
		///
		/// MessageText:
		///
		/// The networking protocol is disabled.%0
		// Token: 0x0400019E RID: 414
		public const int MF_E_NET_PROTOCOL_DISABLED = -1072872812;

		/// MessageId: MF_E_ALREADY_INITIALIZED
		///
		/// MessageText:
		///
		/// This object has already been initialized and cannot be re-initialized at this time.%0
		// Token: 0x0400019F RID: 415
		public const int MF_E_ALREADY_INITIALIZED = -1072871856;

		/// MessageId: MF_E_BANDWIDTH_OVERRUN
		///
		/// MessageText:
		///
		/// The amount of data passed in exceeds the given bitrate and buffer window.%0
		// Token: 0x040001A0 RID: 416
		public const int MF_E_BANDWIDTH_OVERRUN = -1072871855;

		/// MessageId: MF_E_LATE_SAMPLE
		///
		/// MessageText:
		///
		/// The sample was passed in too late to be correctly processed.%0
		// Token: 0x040001A1 RID: 417
		public const int MF_E_LATE_SAMPLE = -1072871854;

		/// MessageId: MF_E_FLUSH_NEEDED
		///
		/// MessageText:
		///
		/// The requested action cannot be carried out until the object is flushed and the queue is emptied.%0
		// Token: 0x040001A2 RID: 418
		public const int MF_E_FLUSH_NEEDED = -1072871853;

		/// MessageId: MF_E_INVALID_PROFILE
		///
		/// MessageText:
		///
		/// The profile is invalid.%0
		// Token: 0x040001A3 RID: 419
		public const int MF_E_INVALID_PROFILE = -1072871852;

		/// MessageId: MF_E_INDEX_NOT_COMMITTED
		///
		/// MessageText:
		///
		/// The index that is being generated needs to be committed before the requested action can be carried out.%0
		// Token: 0x040001A4 RID: 420
		public const int MF_E_INDEX_NOT_COMMITTED = -1072871851;

		/// MessageId: MF_E_NO_INDEX
		///
		/// MessageText:
		///
		/// The index that is necessary for the requested action is not found.%0
		// Token: 0x040001A5 RID: 421
		public const int MF_E_NO_INDEX = -1072871850;

		/// MessageId: MF_E_CANNOT_INDEX_IN_PLACE
		///
		/// MessageText:
		///
		/// The requested index cannot be added in-place to the specified ASF content.%0
		// Token: 0x040001A6 RID: 422
		public const int MF_E_CANNOT_INDEX_IN_PLACE = -1072871849;

		/// MessageId: MF_E_MISSING_ASF_LEAKYBUCKET
		///
		/// MessageText:
		///
		/// The ASF leaky bucket parameters must be specified in order to carry out this request.%0
		// Token: 0x040001A7 RID: 423
		public const int MF_E_MISSING_ASF_LEAKYBUCKET = -1072871848;

		/// MessageId: MF_E_INVALID_ASF_STREAMID
		///
		/// MessageText:
		///
		/// The stream id is invalid. The valid range for ASF stream id is from 1 to 127.%0
		// Token: 0x040001A8 RID: 424
		public const int MF_E_INVALID_ASF_STREAMID = -1072871847;

		/// MessageId: MF_E_STREAMSINK_REMOVED
		///
		/// MessageText:
		///
		/// The requested Stream Sink has been removed and cannot be used.%0
		// Token: 0x040001A9 RID: 425
		public const int MF_E_STREAMSINK_REMOVED = -1072870856;

		/// MessageId: MF_E_STREAMSINKS_OUT_OF_SYNC
		///
		/// MessageText:
		///
		/// The various Stream Sinks in this Media Sink are too far out of sync for the requested action to take place.%0
		// Token: 0x040001AA RID: 426
		public const int MF_E_STREAMSINKS_OUT_OF_SYNC = -1072870854;

		/// MessageId: MF_E_STREAMSINKS_FIXED
		///
		/// MessageText:
		///
		/// Stream Sinks cannot be added to or removed from this Media Sink because its set of streams is fixed.%0
		// Token: 0x040001AB RID: 427
		public const int MF_E_STREAMSINKS_FIXED = -1072870853;

		/// MessageId: MF_E_STREAMSINK_EXISTS
		///
		/// MessageText:
		///
		/// The given Stream Sink already exists.%0
		// Token: 0x040001AC RID: 428
		public const int MF_E_STREAMSINK_EXISTS = -1072870852;

		/// MessageId: MF_E_SAMPLEALLOCATOR_CANCELED
		///
		/// MessageText:
		///
		/// Sample allocations have been canceled.%0
		// Token: 0x040001AD RID: 429
		public const int MF_E_SAMPLEALLOCATOR_CANCELED = -1072870851;

		/// MessageId: MF_E_SAMPLEALLOCATOR_EMPTY
		///
		/// MessageText:
		///
		/// The sample allocator is currently empty, due to outstanding requests.%0
		// Token: 0x040001AE RID: 430
		public const int MF_E_SAMPLEALLOCATOR_EMPTY = -1072870850;

		/// MessageId: MF_E_SINK_ALREADYSTOPPED
		///
		/// MessageText:
		///
		/// When we try to sopt a stream sink, it is already stopped %0
		// Token: 0x040001AF RID: 431
		public const int MF_E_SINK_ALREADYSTOPPED = -1072870849;

		/// MessageId: MF_E_ASF_FILESINK_BITRATE_UNKNOWN
		///
		/// MessageText:
		///
		/// The ASF file sink could not reserve AVIO because the bitrate is unknown.%0
		// Token: 0x040001B0 RID: 432
		public const int MF_E_ASF_FILESINK_BITRATE_UNKNOWN = -1072870848;

		/// MessageId: MF_E_SINK_NO_STREAMS
		///
		/// MessageText:
		///
		/// No streams are selected in sink presentation descriptor.%0
		// Token: 0x040001B1 RID: 433
		public const int MF_E_SINK_NO_STREAMS = -1072870847;

		/// MessageId: MF_S_SINK_NOT_FINALIZED
		///
		/// MessageText:
		///
		/// The sink has not been finalized before shut down. This may cause sink generate a corrupted content.%0
		// Token: 0x040001B2 RID: 434
		public const int MF_S_SINK_NOT_FINALIZED = 870978;

		/// MessageId: MF_E_METADATA_TOO_LONG
		///
		/// MessageText:
		///
		/// A metadata item was too long to write to the output container.%0
		// Token: 0x040001B3 RID: 435
		public const int MF_E_METADATA_TOO_LONG = -1072870845;

		/// MessageId: MF_E_SINK_NO_SAMPLES_PROCESSED
		///
		/// MessageText:
		///
		/// The operation failed because no samples were processed by the sink.%0
		// Token: 0x040001B4 RID: 436
		public const int MF_E_SINK_NO_SAMPLES_PROCESSED = -1072870844;

		/// MessageId: MF_E_VIDEO_REN_NO_PROCAMP_HW
		///
		/// MessageText:
		///
		/// There is no available procamp hardware with which to perform color correction.%0
		// Token: 0x040001B5 RID: 437
		public const int MF_E_VIDEO_REN_NO_PROCAMP_HW = -1072869856;

		/// MessageId: MF_E_VIDEO_REN_NO_DEINTERLACE_HW
		///
		/// MessageText:
		///
		/// There is no available deinterlacing hardware with which to deinterlace the video stream.%0
		// Token: 0x040001B6 RID: 438
		public const int MF_E_VIDEO_REN_NO_DEINTERLACE_HW = -1072869855;

		/// MessageId: MF_E_VIDEO_REN_COPYPROT_FAILED
		///
		/// MessageText:
		///
		/// A video stream requires copy protection to be enabled, but there was a failure in attempting to enable copy protection.%0
		// Token: 0x040001B7 RID: 439
		public const int MF_E_VIDEO_REN_COPYPROT_FAILED = -1072869854;

		/// MessageId: MF_E_VIDEO_REN_SURFACE_NOT_SHARED
		///
		/// MessageText:
		///
		/// A component is attempting to access a surface for sharing that is not shared.%0
		// Token: 0x040001B8 RID: 440
		public const int MF_E_VIDEO_REN_SURFACE_NOT_SHARED = -1072869853;

		/// MessageId: MF_E_VIDEO_DEVICE_LOCKED
		///
		/// MessageText:
		///
		/// A component is attempting to access a shared device that is already locked by another component.%0
		// Token: 0x040001B9 RID: 441
		public const int MF_E_VIDEO_DEVICE_LOCKED = -1072869852;

		/// MessageId: MF_E_NEW_VIDEO_DEVICE
		///
		/// MessageText:
		///
		/// The device is no longer available. The handle should be closed and a new one opened.%0
		// Token: 0x040001BA RID: 442
		public const int MF_E_NEW_VIDEO_DEVICE = -1072869851;

		/// MessageId: MF_E_NO_VIDEO_SAMPLE_AVAILABLE
		///
		/// MessageText:
		///
		/// A video sample is not currently queued on a stream that is required for mixing.%0
		// Token: 0x040001BB RID: 443
		public const int MF_E_NO_VIDEO_SAMPLE_AVAILABLE = -1072869850;

		/// MessageId: MF_E_NO_AUDIO_PLAYBACK_DEVICE
		///
		/// MessageText:
		///
		/// No audio playback device was found.%0
		// Token: 0x040001BC RID: 444
		public const int MF_E_NO_AUDIO_PLAYBACK_DEVICE = -1072869756;

		/// MessageId: MF_E_AUDIO_PLAYBACK_DEVICE_IN_USE
		///
		/// MessageText:
		///
		/// The requested audio playback device is currently in use.%0
		// Token: 0x040001BD RID: 445
		public const int MF_E_AUDIO_PLAYBACK_DEVICE_IN_USE = -1072869755;

		/// MessageId: MF_E_AUDIO_PLAYBACK_DEVICE_INVALIDATED
		///
		/// MessageText:
		///
		/// The audio playback device is no longer present.%0
		// Token: 0x040001BE RID: 446
		public const int MF_E_AUDIO_PLAYBACK_DEVICE_INVALIDATED = -1072869754;

		/// MessageId: MF_E_AUDIO_SERVICE_NOT_RUNNING
		///
		/// MessageText:
		///
		/// The audio service is not running.%0
		// Token: 0x040001BF RID: 447
		public const int MF_E_AUDIO_SERVICE_NOT_RUNNING = -1072869753;

		/// MessageId: MF_E_TOPO_INVALID_OPTIONAL_NODE
		///
		/// MessageText:
		///
		/// The topology contains an invalid optional node.  Possible reasons are incorrect number of outputs and inputs or optional node is at the beginning or end of a segment. %0
		// Token: 0x040001C0 RID: 448
		public const int MF_E_TOPO_INVALID_OPTIONAL_NODE = -1072868850;

		/// MessageId: MF_E_TOPO_CANNOT_FIND_DECRYPTOR
		///
		/// MessageText:
		///
		/// No suitable transform was found to decrypt the content. %0
		// Token: 0x040001C1 RID: 449
		public const int MF_E_TOPO_CANNOT_FIND_DECRYPTOR = -1072868847;

		/// MessageId: MF_E_TOPO_CODEC_NOT_FOUND
		///
		/// MessageText:
		///
		/// No suitable transform was found to encode or decode the content. %0
		// Token: 0x040001C2 RID: 450
		public const int MF_E_TOPO_CODEC_NOT_FOUND = -1072868846;

		/// MessageId: MF_E_TOPO_CANNOT_CONNECT
		///
		/// MessageText:
		///
		/// Unable to find a way to connect nodes%0
		// Token: 0x040001C3 RID: 451
		public const int MF_E_TOPO_CANNOT_CONNECT = -1072868845;

		/// MessageId: MF_E_TOPO_UNSUPPORTED
		///
		/// MessageText:
		///
		/// Unsupported operations in topoloader%0
		// Token: 0x040001C4 RID: 452
		public const int MF_E_TOPO_UNSUPPORTED = -1072868844;

		/// MessageId: MF_E_TOPO_INVALID_TIME_ATTRIBUTES
		///
		/// MessageText:
		///
		/// The topology or its nodes contain incorrectly set time attributes%0
		// Token: 0x040001C5 RID: 453
		public const int MF_E_TOPO_INVALID_TIME_ATTRIBUTES = -1072868843;

		/// MessageId: MF_E_TOPO_LOOPS_IN_TOPOLOGY
		///
		/// MessageText:
		///
		/// The topology contains loops, which are unsupported in media foundation topologies%0
		// Token: 0x040001C6 RID: 454
		public const int MF_E_TOPO_LOOPS_IN_TOPOLOGY = -1072868842;

		/// MessageId: MF_E_TOPO_MISSING_PRESENTATION_DESCRIPTOR
		///
		/// MessageText:
		///
		/// A source stream node in the topology does not have a presentation descriptor%0
		// Token: 0x040001C7 RID: 455
		public const int MF_E_TOPO_MISSING_PRESENTATION_DESCRIPTOR = -1072868841;

		/// MessageId: MF_E_TOPO_MISSING_STREAM_DESCRIPTOR
		///
		/// MessageText:
		///
		/// A source stream node in the topology does not have a stream descriptor%0
		// Token: 0x040001C8 RID: 456
		public const int MF_E_TOPO_MISSING_STREAM_DESCRIPTOR = -1072868840;

		/// MessageId: MF_E_TOPO_STREAM_DESCRIPTOR_NOT_SELECTED
		///
		/// MessageText:
		///
		/// A stream descriptor was set on a source stream node but it was not selected on the presentation descriptor%0
		// Token: 0x040001C9 RID: 457
		public const int MF_E_TOPO_STREAM_DESCRIPTOR_NOT_SELECTED = -1072868839;

		/// MessageId: MF_E_TOPO_MISSING_SOURCE
		///
		/// MessageText:
		///
		/// A source stream node in the topology does not have a source%0
		// Token: 0x040001CA RID: 458
		public const int MF_E_TOPO_MISSING_SOURCE = -1072868838;

		/// MessageId: MF_E_TOPO_SINK_ACTIVATES_UNSUPPORTED
		///
		/// MessageText:
		///
		/// The topology loader does not support sink activates on output nodes.%0
		// Token: 0x040001CB RID: 459
		public const int MF_E_TOPO_SINK_ACTIVATES_UNSUPPORTED = -1072868837;

		/// MessageId: MF_E_SEQUENCER_UNKNOWN_SEGMENT_ID
		///
		/// MessageText:
		///
		/// The sequencer cannot find a segment with the given ID.%0\n.
		// Token: 0x040001CC RID: 460
		public const int MF_E_SEQUENCER_UNKNOWN_SEGMENT_ID = -1072864852;

		/// MessageId: MF_S_SEQUENCER_CONTEXT_CANCELED
		///
		/// MessageText:
		///
		/// The context was canceled.%0\n.
		// Token: 0x040001CD RID: 461
		public const int MF_S_SEQUENCER_CONTEXT_CANCELED = 876973;

		/// MessageId: MF_E_NO_SOURCE_IN_CACHE
		///
		/// MessageText:
		///
		/// Cannot find source in source cache.%0\n.
		// Token: 0x040001CE RID: 462
		public const int MF_E_NO_SOURCE_IN_CACHE = -1072864850;

		/// MessageId: MF_S_SEQUENCER_SEGMENT_AT_END_OF_STREAM
		///
		/// MessageText:
		///
		/// Cannot update topology flags.%0\n.
		// Token: 0x040001CF RID: 463
		public const int MF_S_SEQUENCER_SEGMENT_AT_END_OF_STREAM = 876975;

		/// MessageId: MF_E_TRANSFORM_TYPE_NOT_SET
		///
		/// MessageText:
		///
		/// A valid type has not been set for this stream or a stream that it depends on.%0
		// Token: 0x040001D0 RID: 464
		public const int MF_E_TRANSFORM_TYPE_NOT_SET = -1072861856;

		/// MessageId: MF_E_TRANSFORM_STREAM_CHANGE
		///
		/// MessageText:
		///
		/// A stream change has occurred. Output cannot be produced until the streams have been renegotiated.%0
		// Token: 0x040001D1 RID: 465
		public const int MF_E_TRANSFORM_STREAM_CHANGE = -1072861855;

		/// MessageId: MF_E_TRANSFORM_INPUT_REMAINING
		///
		/// MessageText:
		///
		/// The transform cannot take the requested action until all of the input data it currently holds is processed or flushed.%0
		// Token: 0x040001D2 RID: 466
		public const int MF_E_TRANSFORM_INPUT_REMAINING = -1072861854;

		/// MessageId: MF_E_TRANSFORM_PROFILE_MISSING
		///
		/// MessageText:
		///
		/// The transform requires a profile but no profile was supplied or found.%0
		// Token: 0x040001D3 RID: 467
		public const int MF_E_TRANSFORM_PROFILE_MISSING = -1072861853;

		/// MessageId: MF_E_TRANSFORM_PROFILE_INVALID_OR_CORRUPT
		///
		/// MessageText:
		///
		/// The transform requires a profile but the supplied profile was invalid or corrupt.%0
		// Token: 0x040001D4 RID: 468
		public const int MF_E_TRANSFORM_PROFILE_INVALID_OR_CORRUPT = -1072861852;

		/// MessageId: MF_E_TRANSFORM_PROFILE_TRUNCATED
		///
		/// MessageText:
		///
		/// The transform requires a profile but the supplied profile ended unexpectedly while parsing.%0
		// Token: 0x040001D5 RID: 469
		public const int MF_E_TRANSFORM_PROFILE_TRUNCATED = -1072861851;

		/// MessageId: MF_E_TRANSFORM_PROPERTY_PID_NOT_RECOGNIZED
		///
		/// MessageText:
		///
		/// The property ID does not match any property supported by the transform.%0
		// Token: 0x040001D6 RID: 470
		public const int MF_E_TRANSFORM_PROPERTY_PID_NOT_RECOGNIZED = -1072861850;

		/// MessageId: MF_E_TRANSFORM_PROPERTY_VARIANT_TYPE_WRONG
		///
		/// MessageText:
		///
		/// The variant does not have the type expected for this property ID.%0
		// Token: 0x040001D7 RID: 471
		public const int MF_E_TRANSFORM_PROPERTY_VARIANT_TYPE_WRONG = -1072861849;

		/// MessageId: MF_E_TRANSFORM_PROPERTY_NOT_WRITEABLE
		///
		/// MessageText:
		///
		/// An attempt was made to set the value on a read-only property.%0
		// Token: 0x040001D8 RID: 472
		public const int MF_E_TRANSFORM_PROPERTY_NOT_WRITEABLE = -1072861848;

		/// MessageId: MF_E_TRANSFORM_PROPERTY_ARRAY_VALUE_WRONG_NUM_DIM
		///
		/// MessageText:
		///
		/// The array property value has an unexpected number of dimensions.%0
		// Token: 0x040001D9 RID: 473
		public const int MF_E_TRANSFORM_PROPERTY_ARRAY_VALUE_WRONG_NUM_DIM = -1072861847;

		/// MessageId: MF_E_TRANSFORM_PROPERTY_VALUE_SIZE_WRONG
		///
		/// MessageText:
		///
		/// The array or blob property value has an unexpected size.%0
		// Token: 0x040001DA RID: 474
		public const int MF_E_TRANSFORM_PROPERTY_VALUE_SIZE_WRONG = -1072861846;

		/// MessageId: MF_E_TRANSFORM_PROPERTY_VALUE_OUT_OF_RANGE
		///
		/// MessageText:
		///
		/// The property value is out of range for this transform.%0
		// Token: 0x040001DB RID: 475
		public const int MF_E_TRANSFORM_PROPERTY_VALUE_OUT_OF_RANGE = -1072861845;

		/// MessageId: MF_E_TRANSFORM_PROPERTY_VALUE_INCOMPATIBLE
		///
		/// MessageText:
		///
		/// The property value is incompatible with some other property or mediatype set on the transform.%0
		// Token: 0x040001DC RID: 476
		public const int MF_E_TRANSFORM_PROPERTY_VALUE_INCOMPATIBLE = -1072861844;

		/// MessageId: MF_E_TRANSFORM_NOT_POSSIBLE_FOR_CURRENT_OUTPUT_MEDIATYPE
		///
		/// MessageText:
		///
		/// The requested operation is not supported for the currently set output mediatype.%0
		// Token: 0x040001DD RID: 477
		public const int MF_E_TRANSFORM_NOT_POSSIBLE_FOR_CURRENT_OUTPUT_MEDIATYPE = -1072861843;

		/// MessageId: MF_E_TRANSFORM_NOT_POSSIBLE_FOR_CURRENT_INPUT_MEDIATYPE
		///
		/// MessageText:
		///
		/// The requested operation is not supported for the currently set input mediatype.%0
		// Token: 0x040001DE RID: 478
		public const int MF_E_TRANSFORM_NOT_POSSIBLE_FOR_CURRENT_INPUT_MEDIATYPE = -1072861842;

		/// MessageId: MF_E_TRANSFORM_NOT_POSSIBLE_FOR_CURRENT_MEDIATYPE_COMBINATION
		///
		/// MessageText:
		///
		/// The requested operation is not supported for the currently set combination of mediatypes.%0
		// Token: 0x040001DF RID: 479
		public const int MF_E_TRANSFORM_NOT_POSSIBLE_FOR_CURRENT_MEDIATYPE_COMBINATION = -1072861841;

		/// MessageId: MF_E_TRANSFORM_CONFLICTS_WITH_OTHER_CURRENTLY_ENABLED_FEATURES
		///
		/// MessageText:
		///
		/// The requested feature is not supported in combination with some other currently enabled feature.%0
		// Token: 0x040001E0 RID: 480
		public const int MF_E_TRANSFORM_CONFLICTS_WITH_OTHER_CURRENTLY_ENABLED_FEATURES = -1072861840;

		/// MessageId: MF_E_TRANSFORM_NEED_MORE_INPUT
		///
		/// MessageText:
		///
		/// The transform cannot produce output until it gets more input samples.%0
		// Token: 0x040001E1 RID: 481
		public const int MF_E_TRANSFORM_NEED_MORE_INPUT = -1072861838;

		/// MessageId: MF_E_TRANSFORM_NOT_POSSIBLE_FOR_CURRENT_SPKR_CONFIG
		///
		/// MessageText:
		///
		/// The requested operation is not supported for the current speaker configuration.%0
		// Token: 0x040001E2 RID: 482
		public const int MF_E_TRANSFORM_NOT_POSSIBLE_FOR_CURRENT_SPKR_CONFIG = -1072861837;

		/// MessageId: MF_E_TRANSFORM_CANNOT_CHANGE_MEDIATYPE_WHILE_PROCESSING
		///
		/// MessageText:
		///
		/// The transform cannot accept mediatype changes in the middle of processing.%0
		// Token: 0x040001E3 RID: 483
		public const int MF_E_TRANSFORM_CANNOT_CHANGE_MEDIATYPE_WHILE_PROCESSING = -1072861836;

		/// MessageId: MF_S_TRANSFORM_DO_NOT_PROPAGATE_EVENT
		///
		/// MessageText:
		///
		/// The caller should not propagate this event to downstream components.%0
		// Token: 0x040001E4 RID: 484
		public const int MF_S_TRANSFORM_DO_NOT_PROPAGATE_EVENT = 879989;

		/// MessageId: MF_E_UNSUPPORTED_D3D_TYPE
		///
		/// MessageText:
		///
		/// The input type is not supported for D3D device.%0
		// Token: 0x040001E5 RID: 485
		public const int MF_E_UNSUPPORTED_D3D_TYPE = -1072861834;

		/// MessageId: MF_E_TRANSFORM_ASYNC_LOCKED
		///
		/// MessageText:
		///
		/// The caller does not appear to support this transform's asynchronous capabilities.%0
		// Token: 0x040001E6 RID: 486
		public const int MF_E_TRANSFORM_ASYNC_LOCKED = -1072861833;

		/// MessageId: MF_E_TRANSFORM_CANNOT_INITIALIZE_ACM_DRIVER
		///
		/// MessageText:
		///
		/// An audio compression manager driver could not be initialized by the transform.%0
		// Token: 0x040001E7 RID: 487
		public const int MF_E_TRANSFORM_CANNOT_INITIALIZE_ACM_DRIVER = -1072861832;

		/// MessageId: MF_E_LICENSE_INCORRECT_RIGHTS
		///
		/// MessageText:
		///
		/// You are not allowed to open this file. Contact the content provider for further assistance.%0
		// Token: 0x040001E8 RID: 488
		public const int MF_E_LICENSE_INCORRECT_RIGHTS = -1072860856;

		/// MessageId: MF_E_LICENSE_OUTOFDATE
		///
		/// MessageText:
		///
		/// The license for this media file has expired. Get a new license or contact the content provider for further assistance.%0
		// Token: 0x040001E9 RID: 489
		public const int MF_E_LICENSE_OUTOFDATE = -1072860855;

		/// MessageId: MF_E_LICENSE_REQUIRED
		///
		/// MessageText:
		///
		/// You need a license to perform the requested operation on this media file.%0
		// Token: 0x040001EA RID: 490
		public const int MF_E_LICENSE_REQUIRED = -1072860854;

		/// MessageId: MF_E_DRM_HARDWARE_INCONSISTENT
		///
		/// MessageText:
		///
		/// The licenses for your media files are corrupted. Contact Microsoft product support.%0
		// Token: 0x040001EB RID: 491
		public const int MF_E_DRM_HARDWARE_INCONSISTENT = -1072860853;

		/// MessageId: MF_E_NO_CONTENT_PROTECTION_MANAGER
		///
		/// MessageText:
		///
		/// The APP needs to provide IMFContentProtectionManager callback to access the protected media file.%0
		// Token: 0x040001EC RID: 492
		public const int MF_E_NO_CONTENT_PROTECTION_MANAGER = -1072860852;

		/// MessageId: MF_E_LICENSE_RESTORE_NO_RIGHTS
		///
		/// MessageText:
		///
		/// Client does not have rights to restore licenses.%0
		// Token: 0x040001ED RID: 493
		public const int MF_E_LICENSE_RESTORE_NO_RIGHTS = -1072860851;

		/// MessageId: MF_E_BACKUP_RESTRICTED_LICENSE
		///
		/// MessageText:
		///
		/// Licenses are restricted and hence can not be backed up.%0
		// Token: 0x040001EE RID: 494
		public const int MF_E_BACKUP_RESTRICTED_LICENSE = -1072860850;

		/// MessageId: MF_E_LICENSE_RESTORE_NEEDS_INDIVIDUALIZATION
		///
		/// MessageText:
		///
		/// License restore requires machine to be individualized.%0
		// Token: 0x040001EF RID: 495
		public const int MF_E_LICENSE_RESTORE_NEEDS_INDIVIDUALIZATION = -1072860849;

		/// MessageId: MF_S_PROTECTION_NOT_REQUIRED
		///
		/// MessageText:
		///
		/// Protection for stream is not required.%0
		// Token: 0x040001F0 RID: 496
		public const int MF_S_PROTECTION_NOT_REQUIRED = 880976;

		/// MessageId: MF_E_COMPONENT_REVOKED
		///
		/// MessageText:
		///
		/// Component is revoked.%0
		// Token: 0x040001F1 RID: 497
		public const int MF_E_COMPONENT_REVOKED = -1072860847;

		/// MessageId: MF_E_TRUST_DISABLED
		///
		/// MessageText:
		///
		/// Trusted functionality is currently disabled on this component.%0
		// Token: 0x040001F2 RID: 498
		public const int MF_E_TRUST_DISABLED = -1072860846;

		/// MessageId: MF_E_WMDRMOTA_NO_ACTION
		///
		/// MessageText:
		///
		/// No Action is set on WMDRM Output Trust Authority.%0
		// Token: 0x040001F3 RID: 499
		public const int MF_E_WMDRMOTA_NO_ACTION = -1072860845;

		/// MessageId: MF_E_WMDRMOTA_ACTION_ALREADY_SET
		///
		/// MessageText:
		///
		/// Action is already set on WMDRM Output Trust Authority.%0
		// Token: 0x040001F4 RID: 500
		public const int MF_E_WMDRMOTA_ACTION_ALREADY_SET = -1072860844;

		/// MessageId: MF_E_WMDRMOTA_DRM_HEADER_NOT_AVAILABLE
		///
		/// MessageText:
		///
		/// DRM Heaader is not available.%0
		// Token: 0x040001F5 RID: 501
		public const int MF_E_WMDRMOTA_DRM_HEADER_NOT_AVAILABLE = -1072860843;

		/// MessageId: MF_E_WMDRMOTA_DRM_ENCRYPTION_SCHEME_NOT_SUPPORTED
		///
		/// MessageText:
		///
		/// Current encryption scheme is not supported.%0
		// Token: 0x040001F6 RID: 502
		public const int MF_E_WMDRMOTA_DRM_ENCRYPTION_SCHEME_NOT_SUPPORTED = -1072860842;

		/// MessageId: MF_E_WMDRMOTA_ACTION_MISMATCH
		///
		/// MessageText:
		///
		/// Action does not match with current configuration.%0
		// Token: 0x040001F7 RID: 503
		public const int MF_E_WMDRMOTA_ACTION_MISMATCH = -1072860841;

		/// MessageId: MF_E_WMDRMOTA_INVALID_POLICY
		///
		/// MessageText:
		///
		/// Invalid policy for WMDRM Output Trust Authority.%0
		// Token: 0x040001F8 RID: 504
		public const int MF_E_WMDRMOTA_INVALID_POLICY = -1072860840;

		/// MessageId: MF_E_POLICY_UNSUPPORTED
		///
		/// MessageText:
		///
		/// The policies that the Input Trust Authority requires to be enforced are unsupported by the outputs.%0
		// Token: 0x040001F9 RID: 505
		public const int MF_E_POLICY_UNSUPPORTED = -1072860839;

		/// MessageId: MF_E_OPL_NOT_SUPPORTED
		///
		/// MessageText:
		///
		/// The OPL that the license requires to be enforced are not supported by the Input Trust Authority.%0
		// Token: 0x040001FA RID: 506
		public const int MF_E_OPL_NOT_SUPPORTED = -1072860838;

		/// MessageId: MF_E_TOPOLOGY_VERIFICATION_FAILED
		///
		/// MessageText:
		///
		/// The topology could not be successfully verified.%0
		// Token: 0x040001FB RID: 507
		public const int MF_E_TOPOLOGY_VERIFICATION_FAILED = -1072860837;

		/// MessageId: MF_E_SIGNATURE_VERIFICATION_FAILED
		///
		/// MessageText:
		///
		/// Signature verification could not be completed successfully for this component.%0
		// Token: 0x040001FC RID: 508
		public const int MF_E_SIGNATURE_VERIFICATION_FAILED = -1072860836;

		/// MessageId: MF_E_DEBUGGING_NOT_ALLOWED
		///
		/// MessageText:
		///
		/// Running this process under a debugger while using protected content is not allowed.%0
		// Token: 0x040001FD RID: 509
		public const int MF_E_DEBUGGING_NOT_ALLOWED = -1072860835;

		/// MessageId: MF_E_CODE_EXPIRED
		///
		/// MessageText:
		///
		/// MF component has expired.%0
		// Token: 0x040001FE RID: 510
		public const int MF_E_CODE_EXPIRED = -1072860834;

		/// MessageId: MF_E_GRL_VERSION_TOO_LOW
		///
		/// MessageText:
		///
		/// The current GRL on the machine does not meet the minimum version requirements.%0
		// Token: 0x040001FF RID: 511
		public const int MF_E_GRL_VERSION_TOO_LOW = -1072860833;

		/// MessageId: MF_E_GRL_RENEWAL_NOT_FOUND
		///
		/// MessageText:
		///
		/// The current GRL on the machine does not contain any renewal entries for the specified revocation.%0
		// Token: 0x04000200 RID: 512
		public const int MF_E_GRL_RENEWAL_NOT_FOUND = -1072860832;

		/// MessageId: MF_E_GRL_EXTENSIBLE_ENTRY_NOT_FOUND
		///
		/// MessageText:
		///
		/// The current GRL on the machine does not contain any extensible entries for the specified extension GUID.%0
		// Token: 0x04000201 RID: 513
		public const int MF_E_GRL_EXTENSIBLE_ENTRY_NOT_FOUND = -1072860831;

		/// MessageId: MF_E_KERNEL_UNTRUSTED
		///
		/// MessageText:
		///
		/// The kernel isn't secure for high security level content.%0
		// Token: 0x04000202 RID: 514
		public const int MF_E_KERNEL_UNTRUSTED = -1072860830;

		/// MessageId: MF_E_PEAUTH_UNTRUSTED
		///
		/// MessageText:
		///
		/// The response from protected environment driver isn't valid.%0
		// Token: 0x04000203 RID: 515
		public const int MF_E_PEAUTH_UNTRUSTED = -1072860829;

		/// MessageId: MF_E_NON_PE_PROCESS
		///
		/// MessageText:
		///
		/// A non-PE process tried to talk to PEAuth.%0
		// Token: 0x04000204 RID: 516
		public const int MF_E_NON_PE_PROCESS = -1072860827;

		/// MessageId: MF_E_REBOOT_REQUIRED
		///
		/// MessageText:
		///
		/// We need to reboot the machine.%0
		// Token: 0x04000205 RID: 517
		public const int MF_E_REBOOT_REQUIRED = -1072860825;

		/// MessageId: MF_S_WAIT_FOR_POLICY_SET
		///
		/// MessageText:
		///
		/// Protection for this stream is not guaranteed to be enforced until the MEPolicySet event is fired.%0
		// Token: 0x04000206 RID: 518
		public const int MF_S_WAIT_FOR_POLICY_SET = 881000;

		/// MessageId: MF_S_VIDEO_DISABLED_WITH_UNKNOWN_SOFTWARE_OUTPUT
		///
		/// MessageText:
		///
		/// This video stream is disabled because it is being sent to an unknown software output.%0
		// Token: 0x04000207 RID: 519
		public const int MF_S_VIDEO_DISABLED_WITH_UNKNOWN_SOFTWARE_OUTPUT = 881001;

		/// MessageId: MF_E_GRL_INVALID_FORMAT
		///
		/// MessageText:
		///
		/// The GRL file is not correctly formed, it may have been corrupted or overwritten.%0
		// Token: 0x04000208 RID: 520
		public const int MF_E_GRL_INVALID_FORMAT = -1072860822;

		/// MessageId: MF_E_GRL_UNRECOGNIZED_FORMAT
		///
		/// MessageText:
		///
		/// The GRL file is in a format newer than those recognized by this GRL Reader.%0
		// Token: 0x04000209 RID: 521
		public const int MF_E_GRL_UNRECOGNIZED_FORMAT = -1072860821;

		/// MessageId: MF_E_ALL_PROCESS_RESTART_REQUIRED
		///
		/// MessageText:
		///
		/// The GRL was reloaded and required all processes that can run protected media to restart.%0
		// Token: 0x0400020A RID: 522
		public const int MF_E_ALL_PROCESS_RESTART_REQUIRED = -1072860820;

		/// MessageId: MF_E_PROCESS_RESTART_REQUIRED
		///
		/// MessageText:
		///
		/// The GRL was reloaded and the current process needs to restart.%0
		// Token: 0x0400020B RID: 523
		public const int MF_E_PROCESS_RESTART_REQUIRED = -1072860819;

		/// MessageId: MF_E_USERMODE_UNTRUSTED
		///
		/// MessageText:
		///
		/// The user space is untrusted for protected content play.%0
		// Token: 0x0400020C RID: 524
		public const int MF_E_USERMODE_UNTRUSTED = -1072860818;

		/// MessageId: MF_E_PEAUTH_SESSION_NOT_STARTED
		///
		/// MessageText:
		///
		/// PEAuth communication session hasn't been started.%0
		// Token: 0x0400020D RID: 525
		public const int MF_E_PEAUTH_SESSION_NOT_STARTED = -1072860817;

		/// MessageId: MF_E_PEAUTH_PUBLICKEY_REVOKED
		///
		/// MessageText:
		///
		/// PEAuth's public key is revoked.%0
		// Token: 0x0400020E RID: 526
		public const int MF_E_PEAUTH_PUBLICKEY_REVOKED = -1072860815;

		/// MessageId: MF_E_GRL_ABSENT
		///
		/// MessageText:
		///
		/// The GRL is absent.%0
		// Token: 0x0400020F RID: 527
		public const int MF_E_GRL_ABSENT = -1072860814;

		/// MessageId: MF_S_PE_TRUSTED
		///
		/// MessageText:
		///
		/// The Protected Environment is trusted.%0
		// Token: 0x04000210 RID: 528
		public const int MF_S_PE_TRUSTED = 881011;

		/// MessageId: MF_E_PE_UNTRUSTED
		///
		/// MessageText:
		///
		/// The Protected Environment is untrusted.%0
		// Token: 0x04000211 RID: 529
		public const int MF_E_PE_UNTRUSTED = -1072860812;

		/// MessageId: MF_E_PEAUTH_NOT_STARTED
		///
		/// MessageText:
		///
		/// The Protected Environment Authorization service (PEAUTH) has not been started.%0
		// Token: 0x04000212 RID: 530
		public const int MF_E_PEAUTH_NOT_STARTED = -1072860811;

		/// MessageId: MF_E_INCOMPATIBLE_SAMPLE_PROTECTION
		///
		/// MessageText:
		///
		/// The sample protection algorithms supported by components are not compatible.%0
		// Token: 0x04000213 RID: 531
		public const int MF_E_INCOMPATIBLE_SAMPLE_PROTECTION = -1072860810;

		/// MessageId: MF_E_PE_SESSIONS_MAXED
		///
		/// MessageText:
		///
		/// No more protected environment sessions can be supported.%0
		// Token: 0x04000214 RID: 532
		public const int MF_E_PE_SESSIONS_MAXED = -1072860809;

		/// MessageId: MF_E_HIGH_SECURITY_LEVEL_CONTENT_NOT_ALLOWED
		///
		/// MessageText:
		///
		/// WMDRM ITA does not allow protected content with high security level for this release.%0
		// Token: 0x04000215 RID: 533
		public const int MF_E_HIGH_SECURITY_LEVEL_CONTENT_NOT_ALLOWED = -1072860808;

		/// MessageId: MF_E_TEST_SIGNED_COMPONENTS_NOT_ALLOWED
		///
		/// MessageText:
		///
		/// WMDRM ITA cannot allow the requested action for the content as one or more components is not properly signed.%0
		// Token: 0x04000216 RID: 534
		public const int MF_E_TEST_SIGNED_COMPONENTS_NOT_ALLOWED = -1072860807;

		/// MessageId: MF_E_ITA_UNSUPPORTED_ACTION
		///
		/// MessageText:
		///
		/// WMDRM ITA does not support the requested action.%0
		// Token: 0x04000217 RID: 535
		public const int MF_E_ITA_UNSUPPORTED_ACTION = -1072860806;

		/// MessageId: MF_E_ITA_ERROR_PARSING_SAP_PARAMETERS
		///
		/// MessageText:
		///
		/// WMDRM ITA encountered an error in parsing the Secure Audio Path parameters.%0
		// Token: 0x04000218 RID: 536
		public const int MF_E_ITA_ERROR_PARSING_SAP_PARAMETERS = -1072860805;

		/// MessageId: MF_E_POLICY_MGR_ACTION_OUTOFBOUNDS
		///
		/// MessageText:
		///
		/// The Policy Manager action passed in is invalid.%0
		// Token: 0x04000219 RID: 537
		public const int MF_E_POLICY_MGR_ACTION_OUTOFBOUNDS = -1072860804;

		/// MessageId: MF_E_BAD_OPL_STRUCTURE_FORMAT
		///
		/// MessageText:
		///
		/// The structure specifying Output Protection Level is not the correct format.%0
		// Token: 0x0400021A RID: 538
		public const int MF_E_BAD_OPL_STRUCTURE_FORMAT = -1072860803;

		/// MessageId: MF_E_ITA_UNRECOGNIZED_ANALOG_VIDEO_PROTECTION_GUID
		///
		/// MessageText:
		///
		/// WMDRM ITA does not recognize the Explicite Analog Video Output Protection guid specified in the license.%0
		// Token: 0x0400021B RID: 539
		public const int MF_E_ITA_UNRECOGNIZED_ANALOG_VIDEO_PROTECTION_GUID = -1072860802;

		/// MessageId: MF_E_NO_PMP_HOST
		///
		/// MessageText:
		///
		/// IMFPMPHost object not available.%0
		// Token: 0x0400021C RID: 540
		public const int MF_E_NO_PMP_HOST = -1072860801;

		/// MessageId: MF_E_ITA_OPL_DATA_NOT_INITIALIZED
		///
		/// MessageText:
		///
		/// WMDRM ITA could not initialize the Output Protection Level data.%0
		// Token: 0x0400021D RID: 541
		public const int MF_E_ITA_OPL_DATA_NOT_INITIALIZED = -1072860800;

		/// MessageId: MF_E_ITA_UNRECOGNIZED_ANALOG_VIDEO_OUTPUT
		///
		/// MessageText:
		///
		/// WMDRM ITA does not recognize the Analog Video Output specified by the OTA.%0
		// Token: 0x0400021E RID: 542
		public const int MF_E_ITA_UNRECOGNIZED_ANALOG_VIDEO_OUTPUT = -1072860799;

		/// MessageId: MF_E_ITA_UNRECOGNIZED_DIGITAL_VIDEO_OUTPUT
		///
		/// MessageText:
		///
		/// WMDRM ITA does not recognize the Digital Video Output specified by the OTA.%0
		// Token: 0x0400021F RID: 543
		public const int MF_E_ITA_UNRECOGNIZED_DIGITAL_VIDEO_OUTPUT = -1072860798;

		/// MessageId: MF_E_CLOCK_INVALID_CONTINUITY_KEY
		///
		/// MessageText:
		///
		/// The continuity key supplied is not currently valid.%0
		// Token: 0x04000220 RID: 544
		public const int MF_E_CLOCK_INVALID_CONTINUITY_KEY = -1072849856;

		/// MessageId: MF_E_CLOCK_NO_TIME_SOURCE
		///
		/// MessageText:
		///
		/// No Presentation Time Source has been specified.%0
		// Token: 0x04000221 RID: 545
		public const int MF_E_CLOCK_NO_TIME_SOURCE = -1072849855;

		/// MessageId: MF_E_CLOCK_STATE_ALREADY_SET
		///
		/// MessageText:
		///
		/// The clock is already in the requested state.%0
		// Token: 0x04000222 RID: 546
		public const int MF_E_CLOCK_STATE_ALREADY_SET = -1072849854;

		/// MessageId: MF_E_CLOCK_NOT_SIMPLE
		///
		/// MessageText:
		///
		/// The clock has too many advanced features to carry out the request.%0
		// Token: 0x04000223 RID: 547
		public const int MF_E_CLOCK_NOT_SIMPLE = -1072849853;

		/// MessageId: MF_S_CLOCK_STOPPED
		///
		/// MessageText:
		///
		/// Timer::SetTimer returns this success code if called happened while timer is stopped. Timer is not going to be dispatched until clock is running%0
		// Token: 0x04000224 RID: 548
		public const int MF_S_CLOCK_STOPPED = 891972;

		/// MessageId: MF_E_NO_MORE_DROP_MODES
		///
		/// MessageText:
		///
		/// The component does not support any more drop modes.%0
		// Token: 0x04000225 RID: 549
		public const int MF_E_NO_MORE_DROP_MODES = -1072848856;

		/// MessageId: MF_E_NO_MORE_QUALITY_LEVELS
		///
		/// MessageText:
		///
		/// The component does not support any more quality levels.%0
		// Token: 0x04000226 RID: 550
		public const int MF_E_NO_MORE_QUALITY_LEVELS = -1072848855;

		/// MessageId: MF_E_DROPTIME_NOT_SUPPORTED
		///
		/// MessageText:
		///
		/// The component does not support drop time functionality.%0
		// Token: 0x04000227 RID: 551
		public const int MF_E_DROPTIME_NOT_SUPPORTED = -1072848854;

		/// MessageId: MF_E_QUALITYKNOB_WAIT_LONGER
		///
		/// MessageText:
		///
		/// Quality Manager needs to wait longer before bumping the Quality Level up.%0
		// Token: 0x04000228 RID: 552
		public const int MF_E_QUALITYKNOB_WAIT_LONGER = -1072848853;

		/// MessageId: MF_E_QM_INVALIDSTATE
		///
		/// MessageText:
		///
		/// Quality Manager is in an invalid state. Quality Management is off at this moment.%0
		// Token: 0x04000229 RID: 553
		public const int MF_E_QM_INVALIDSTATE = -1072848852;

		/// MessageId: MF_E_TRANSCODE_NO_CONTAINERTYPE
		///
		/// MessageText:
		///
		/// No transcode output container type is specified.%0
		// Token: 0x0400022A RID: 554
		public const int MF_E_TRANSCODE_NO_CONTAINERTYPE = -1072847856;

		/// MessageId: MF_E_TRANSCODE_PROFILE_NO_MATCHING_STREAMS
		///
		/// MessageText:
		///
		/// The profile does not have a media type configuration for any selected source streams.%0
		// Token: 0x0400022B RID: 555
		public const int MF_E_TRANSCODE_PROFILE_NO_MATCHING_STREAMS = -1072847855;

		/// MessageId: MF_E_TRANSCODE_NO_MATCHING_ENCODER
		///
		/// MessageText:
		///
		/// Cannot find an encoder MFT that accepts the user preferred output type.%0
		// Token: 0x0400022C RID: 556
		public const int MF_E_TRANSCODE_NO_MATCHING_ENCODER = -1072847854;

		/// MessageId: MF_E_ALLOCATOR_NOT_INITIALIZED
		///
		/// MessageText:
		///
		/// Memory allocator is not initialized.%0
		// Token: 0x0400022D RID: 557
		public const int MF_E_ALLOCATOR_NOT_INITIALIZED = -1072846856;

		/// MessageId: MF_E_ALLOCATOR_NOT_COMMITED
		///
		/// MessageText:
		///
		/// Memory allocator is not committed yet.%0
		// Token: 0x0400022E RID: 558
		public const int MF_E_ALLOCATOR_NOT_COMMITED = -1072846855;

		/// MessageId: MF_E_ALLOCATOR_ALREADY_COMMITED
		///
		/// MessageText:
		///
		/// Memory allocator has already been committed.%0
		// Token: 0x0400022F RID: 559
		public const int MF_E_ALLOCATOR_ALREADY_COMMITED = -1072846854;

		/// MessageId: MF_E_STREAM_ERROR
		///
		/// MessageText:
		///
		/// An error occurred in media stream.%0
		// Token: 0x04000230 RID: 560
		public const int MF_E_STREAM_ERROR = -1072846853;

		/// MessageId: MF_E_INVALID_STREAM_STATE
		///
		/// MessageText:
		///
		/// Stream is not in a state to handle the request.%0
		// Token: 0x04000231 RID: 561
		public const int MF_E_INVALID_STREAM_STATE = -1072846852;

		/// MessageId: MF_E_HW_STREAM_NOT_CONNECTED
		///
		/// MessageText:
		///
		/// Hardware stream is not connected yet.%0
		// Token: 0x04000232 RID: 562
		public const int MF_E_HW_STREAM_NOT_CONNECTED = -1072846851;
	}
}
