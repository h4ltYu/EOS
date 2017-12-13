using System;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// See mfobjects.h
	/// </summary>
	// Token: 0x0200004C RID: 76
	public enum MediaEventType
	{
		/// <summary>
		/// Unknown event type.
		/// </summary>
		// Token: 0x0400023F RID: 575
		MEUnknown,
		/// <summary>
		/// Signals a serious error.
		/// </summary>
		// Token: 0x04000240 RID: 576
		MEError,
		/// <summary>
		/// Custom event type.
		/// </summary>
		// Token: 0x04000241 RID: 577
		MEExtendedType,
		/// <summary>
		/// A non-fatal error occurred during streaming.
		/// </summary>
		// Token: 0x04000242 RID: 578
		MENonFatalError,
		/// <summary>
		/// Session Unknown
		/// </summary>
		// Token: 0x04000243 RID: 579
		MESessionUnknown = 100,
		/// <summary>
		/// Raised after the IMFMediaSession::SetTopology method completes asynchronously
		/// </summary>
		// Token: 0x04000244 RID: 580
		MESessionTopologySet,
		/// <summary>
		/// Raised by the Media Session when the IMFMediaSession::ClearTopologies method completes asynchronously.
		/// </summary>
		// Token: 0x04000245 RID: 581
		MESessionTopologiesCleared,
		/// <summary>
		/// Raised when the IMFMediaSession::Start method completes asynchronously.
		/// </summary>
		// Token: 0x04000246 RID: 582
		MESessionStarted,
		/// <summary>
		/// Raised when the IMFMediaSession::Pause method completes asynchronously.
		/// </summary>
		// Token: 0x04000247 RID: 583
		MESessionPaused,
		/// <summary>
		/// Raised when the IMFMediaSession::Stop method completes asynchronously.
		/// </summary>
		// Token: 0x04000248 RID: 584
		MESessionStopped,
		/// <summary>
		/// Raised when the IMFMediaSession::Close method completes asynchronously.
		/// </summary>
		// Token: 0x04000249 RID: 585
		MESessionClosed,
		/// <summary>
		/// Raised by the Media Session when it has finished playing the last presentation in the playback queue.
		/// </summary>
		// Token: 0x0400024A RID: 586
		MESessionEnded,
		/// <summary>
		/// Raised by the Media Session when the playback rate changes.
		/// </summary>
		// Token: 0x0400024B RID: 587
		MESessionRateChanged,
		/// <summary>
		/// Raised by the Media Session when it completes a scrubbing request.
		/// </summary>
		// Token: 0x0400024C RID: 588
		MESessionScrubSampleComplete,
		/// <summary>
		/// Raised by the Media Session when the session capabilities change.
		/// </summary>
		// Token: 0x0400024D RID: 589
		MESessionCapabilitiesChanged,
		/// <summary>
		/// Raised by the Media Session when the status of a topology changes.
		/// </summary>
		// Token: 0x0400024E RID: 590
		MESessionTopologyStatus,
		/// <summary>
		/// Raised by the Media Session when a new presentation starts.
		/// </summary>
		// Token: 0x0400024F RID: 591
		MESessionNotifyPresentationTime,
		/// <summary>
		/// Raised by a media source a new presentation is ready.
		/// </summary>
		// Token: 0x04000250 RID: 592
		MENewPresentation,
		/// <summary>
		/// License acquisition is about to begin.
		/// </summary>
		// Token: 0x04000251 RID: 593
		MELicenseAcquisitionStart,
		/// <summary>
		/// License acquisition is complete.
		/// </summary>
		// Token: 0x04000252 RID: 594
		MELicenseAcquisitionCompleted,
		/// <summary>
		/// Individualization is about to begin.
		/// </summary>
		// Token: 0x04000253 RID: 595
		MEIndividualizationStart,
		/// <summary>
		/// Individualization is complete.
		/// </summary>
		// Token: 0x04000254 RID: 596
		MEIndividualizationCompleted,
		/// <summary>
		/// Signals the progress of a content enabler object.
		/// </summary>
		// Token: 0x04000255 RID: 597
		MEEnablerProgress,
		/// <summary>
		/// A content enabler object's action is complete.
		/// </summary>
		// Token: 0x04000256 RID: 598
		MEEnablerCompleted,
		/// <summary>
		/// Raised by a trusted output if an error occurs while enforcing the output policy.
		/// </summary>
		// Token: 0x04000257 RID: 599
		MEPolicyError,
		/// <summary>
		/// Contains status information about the enforcement of an output policy.
		/// </summary>
		// Token: 0x04000258 RID: 600
		MEPolicyReport,
		/// <summary>
		/// A media source started to buffer data.
		/// </summary>
		// Token: 0x04000259 RID: 601
		MEBufferingStarted,
		/// <summary>
		/// A media source stopped buffering data.
		/// </summary>
		// Token: 0x0400025A RID: 602
		MEBufferingStopped,
		/// <summary>
		/// The network source started opening a URL.
		/// </summary>
		// Token: 0x0400025B RID: 603
		MEConnectStart,
		/// <summary>
		/// The network source finished opening a URL.
		/// </summary>
		// Token: 0x0400025C RID: 604
		MEConnectEnd,
		/// <summary>
		/// Raised by a media source at the start of a reconnection attempt.
		/// </summary>
		// Token: 0x0400025D RID: 605
		MEReconnectStart,
		/// <summary>
		/// Raised by a media source at the end of a reconnection attempt.
		/// </summary>
		// Token: 0x0400025E RID: 606
		MEReconnectEnd,
		/// <summary>
		/// Raised by the enhanced video renderer (EVR) when it receives a user event from the presenter.
		/// </summary>
		// Token: 0x0400025F RID: 607
		MERendererEvent,
		/// <summary>
		/// Raised by the Media Session when the format changes on a media sink.
		/// </summary>
		// Token: 0x04000260 RID: 608
		MESessionStreamSinkFormatChanged,
		/// <summary>
		/// Source Unknown
		/// </summary>
		// Token: 0x04000261 RID: 609
		MESourceUnknown = 200,
		/// <summary>
		/// Raised when a media source starts without seeking.
		/// </summary>
		// Token: 0x04000262 RID: 610
		MESourceStarted,
		/// <summary>
		/// Raised by a media stream when the source starts without seeking.
		/// </summary>
		// Token: 0x04000263 RID: 611
		MEStreamStarted,
		/// <summary>
		/// Raised when a media source seeks to a new position.
		/// </summary>
		// Token: 0x04000264 RID: 612
		MESourceSeeked,
		/// <summary>
		/// Raised by a media stream after a call to IMFMediaSource::Start causes a seek in the stream.
		/// </summary>
		// Token: 0x04000265 RID: 613
		MEStreamSeeked,
		/// <summary>
		/// Raised by a media source when it starts a new stream.
		/// </summary>
		// Token: 0x04000266 RID: 614
		MENewStream,
		/// <summary>
		/// Raised by a media source when it restarts or seeks a stream that is already active.
		/// </summary>
		// Token: 0x04000267 RID: 615
		MEUpdatedStream,
		/// <summary>
		/// Raised by a media source when the IMFMediaSource::Stop method completes asynchronously.
		/// </summary>
		// Token: 0x04000268 RID: 616
		MESourceStopped,
		/// <summary>
		/// Raised by a media stream when the IMFMediaSource::Stop method completes asynchronously.
		/// </summary>
		// Token: 0x04000269 RID: 617
		MEStreamStopped,
		/// <summary>
		/// Raised by a media source when the IMFMediaSource::Pause method completes asynchronously.
		/// </summary>
		// Token: 0x0400026A RID: 618
		MESourcePaused,
		/// <summary>
		/// Raised by a media stream when the IMFMediaSource::Pause method completes asynchronously.
		/// </summary>
		// Token: 0x0400026B RID: 619
		MEStreamPaused,
		/// <summary>
		/// Raised by a media source when a presentation ends.
		/// </summary>
		// Token: 0x0400026C RID: 620
		MEEndOfPresentation,
		/// <summary>
		/// Raised by a media stream when the stream ends.
		/// </summary>
		// Token: 0x0400026D RID: 621
		MEEndOfStream,
		/// <summary>
		/// Raised when a media stream delivers a new sample.
		/// </summary>
		// Token: 0x0400026E RID: 622
		MEMediaSample,
		/// <summary>
		/// Signals that a media stream does not have data available at a specified time.
		/// </summary>
		// Token: 0x0400026F RID: 623
		MEStreamTick,
		/// <summary>
		/// Raised by a media stream when it starts or stops thinning the stream.
		/// </summary>
		// Token: 0x04000270 RID: 624
		MEStreamThinMode,
		/// <summary>
		/// Raised by a media stream when the media type of the stream changes.
		/// </summary>
		// Token: 0x04000271 RID: 625
		MEStreamFormatChanged,
		/// <summary>
		/// Raised by a media source when the playback rate changes.
		/// </summary>
		// Token: 0x04000272 RID: 626
		MESourceRateChanged,
		/// <summary>
		/// Raised by the sequencer source when a segment is completed and is followed by another segment.
		/// </summary>
		// Token: 0x04000273 RID: 627
		MEEndOfPresentationSegment,
		/// <summary>
		/// Raised by a media source when the source's characteristics change.
		/// </summary>
		// Token: 0x04000274 RID: 628
		MESourceCharacteristicsChanged,
		/// <summary>
		/// Raised by a media source to request a new playback rate.
		/// </summary>
		// Token: 0x04000275 RID: 629
		MESourceRateChangeRequested,
		/// <summary>
		/// Raised by a media source when it updates its metadata.
		/// </summary>
		// Token: 0x04000276 RID: 630
		MESourceMetadataChanged,
		/// <summary>
		/// Raised by the sequencer source when the IMFSequencerSource::UpdateTopology method completes asynchronously.
		/// </summary>
		// Token: 0x04000277 RID: 631
		MESequencerSourceTopologyUpdated,
		/// <summary>
		/// Sink Unknown
		/// </summary>
		// Token: 0x04000278 RID: 632
		MESinkUnknown = 300,
		/// <summary>
		/// Raised by a stream sink when it completes the transition to the running state.
		/// </summary>
		// Token: 0x04000279 RID: 633
		MEStreamSinkStarted,
		/// <summary>
		/// Raised by a stream sink when it completes the transition to the stopped state.
		/// </summary>
		// Token: 0x0400027A RID: 634
		MEStreamSinkStopped,
		/// <summary>
		/// Raised by a stream sink when it completes the transition to the paused state.
		/// </summary>
		// Token: 0x0400027B RID: 635
		MEStreamSinkPaused,
		/// <summary>
		/// Raised by a stream sink when the rate has changed.
		/// </summary>
		// Token: 0x0400027C RID: 636
		MEStreamSinkRateChanged,
		/// <summary>
		/// Raised by a stream sink to request a new media sample from the pipeline.
		/// </summary>
		// Token: 0x0400027D RID: 637
		MEStreamSinkRequestSample,
		/// <summary>
		/// Raised by a stream sink after the IMFStreamSink::PlaceMarker method is called.
		/// </summary>
		// Token: 0x0400027E RID: 638
		MEStreamSinkMarker,
		/// <summary>
		/// Raised by a stream sink when the stream has received enough preroll data to begin rendering.
		/// </summary>
		// Token: 0x0400027F RID: 639
		MEStreamSinkPrerolled,
		/// <summary>
		/// Raised by a stream sink when it completes a scrubbing request.
		/// </summary>
		// Token: 0x04000280 RID: 640
		MEStreamSinkScrubSampleComplete,
		/// <summary>
		/// Raised by a stream sink when the sink's media type is no longer valid.
		/// </summary>
		// Token: 0x04000281 RID: 641
		MEStreamSinkFormatChanged,
		/// <summary>
		/// Raised by the stream sinks of the EVR if the video device changes.
		/// </summary>
		// Token: 0x04000282 RID: 642
		MEStreamSinkDeviceChanged,
		/// <summary>
		/// Provides feedback about playback quality to the quality manager.
		/// </summary>
		// Token: 0x04000283 RID: 643
		MEQualityNotify,
		/// <summary>
		/// Raised when a media sink becomes invalid.
		/// </summary>
		// Token: 0x04000284 RID: 644
		MESinkInvalidated,
		/// <summary>
		/// The audio session display name changed.
		/// </summary>
		// Token: 0x04000285 RID: 645
		MEAudioSessionNameChanged,
		/// <summary>
		/// The volume or mute state of the audio session changed
		/// </summary>
		// Token: 0x04000286 RID: 646
		MEAudioSessionVolumeChanged,
		/// <summary>
		/// The audio device was removed.
		/// </summary>
		// Token: 0x04000287 RID: 647
		MEAudioSessionDeviceRemoved,
		/// <summary>
		/// The Windows audio server system was shut down.
		/// </summary>
		// Token: 0x04000288 RID: 648
		MEAudioSessionServerShutdown,
		/// <summary>
		/// The grouping parameters changed for the audio session.
		/// </summary>
		// Token: 0x04000289 RID: 649
		MEAudioSessionGroupingParamChanged,
		/// <summary>
		/// The audio session icon changed.
		/// </summary>
		// Token: 0x0400028A RID: 650
		MEAudioSessionIconChanged,
		/// <summary>
		/// The default audio format for the audio device changed.
		/// </summary>
		// Token: 0x0400028B RID: 651
		MEAudioSessionFormatChanged,
		/// <summary>
		/// The audio session was disconnected from a Windows Terminal Services session
		/// </summary>
		// Token: 0x0400028C RID: 652
		MEAudioSessionDisconnected,
		/// <summary>
		/// The audio session was preempted by an exclusive-mode connection.
		/// </summary>
		// Token: 0x0400028D RID: 653
		MEAudioSessionExclusiveModeOverride,
		/// <summary>
		/// Trust Unknown
		/// </summary>
		// Token: 0x0400028E RID: 654
		METrustUnknown = 400,
		/// <summary>
		/// The output policy for a stream changed.
		/// </summary>
		// Token: 0x0400028F RID: 655
		MEPolicyChanged,
		/// <summary>
		/// Content protection message
		/// </summary>
		// Token: 0x04000290 RID: 656
		MEContentProtectionMessage,
		/// <summary>
		/// The IMFOutputTrustAuthority::SetPolicy method completed.
		/// </summary>
		// Token: 0x04000291 RID: 657
		MEPolicySet,
		/// <summary>
		/// DRM License Backup Completed
		/// </summary>
		// Token: 0x04000292 RID: 658
		MEWMDRMLicenseBackupCompleted = 500,
		/// <summary>
		/// DRM License Backup Progress
		/// </summary>
		// Token: 0x04000293 RID: 659
		MEWMDRMLicenseBackupProgress,
		/// <summary>
		/// DRM License Restore Completed
		/// </summary>
		// Token: 0x04000294 RID: 660
		MEWMDRMLicenseRestoreCompleted,
		/// <summary>
		/// DRM License Restore Progress
		/// </summary>
		// Token: 0x04000295 RID: 661
		MEWMDRMLicenseRestoreProgress,
		/// <summary>
		/// DRM License Acquisition Completed
		/// </summary>
		// Token: 0x04000296 RID: 662
		MEWMDRMLicenseAcquisitionCompleted = 506,
		/// <summary>
		/// DRM Individualization Completed
		/// </summary>
		// Token: 0x04000297 RID: 663
		MEWMDRMIndividualizationCompleted = 508,
		/// <summary>
		/// DRM Individualization Progress
		/// </summary>
		// Token: 0x04000298 RID: 664
		MEWMDRMIndividualizationProgress = 513,
		/// <summary>
		/// DRM Proximity Completed
		/// </summary>
		// Token: 0x04000299 RID: 665
		MEWMDRMProximityCompleted,
		/// <summary>
		/// DRM License Store Cleaned
		/// </summary>
		// Token: 0x0400029A RID: 666
		MEWMDRMLicenseStoreCleaned,
		/// <summary>
		/// DRM Revocation Download Completed
		/// </summary>
		// Token: 0x0400029B RID: 667
		MEWMDRMRevocationDownloadCompleted,
		/// <summary>
		/// Transform Unknown
		/// </summary>
		// Token: 0x0400029C RID: 668
		METransformUnknown = 600,
		/// <summary>
		/// Sent by an asynchronous MFT to request a new input sample.
		/// </summary>
		// Token: 0x0400029D RID: 669
		METransformNeedInput,
		/// <summary>
		/// Sent by an asynchronous MFT when new output data is available from the MFT.
		/// </summary>
		// Token: 0x0400029E RID: 670
		METransformHaveOutput,
		/// <summary>
		/// Sent by an asynchronous Media Foundation transform (MFT) when a drain operation is complete.
		/// </summary>
		// Token: 0x0400029F RID: 671
		METransformDrainComplete,
		/// <summary>
		/// Sent by an asynchronous MFT in response to an MFT_MESSAGE_COMMAND_MARKER message.
		/// </summary>
		// Token: 0x040002A0 RID: 672
		METransformMarker
	}
}
