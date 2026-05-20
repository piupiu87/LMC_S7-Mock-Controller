namespace AdlinkMockController
{
    // Loaded from axes.json; describes which axes to show and their display names.
    public class AxisConfig
    {
        public int    AxisId { get; set; }
        public string Name   { get; set; } = string.Empty;
    }

    public enum APS_4XMO_Motion_IO_Status
    {
        MIO_ALM  = 0x1,     // Servo alarm.
        MIO_PEL  = 0x2,     // Positive end limit.
        MIO_MEL  = 0x4,     // Negative end limit.
        MIO_ORG  = 0x8,     // ORG (Home)
        MIO_EMG  = 0x10,    // Emergency stop
        MIO_EZ   = 0x20,    // EZ.
        MIO_INP  = 0x40,    // In position.
        MIO_SVON = 0x80,    // Servo on signal.
        MIO_RDY  = 0x100,   // Ready.
        MIO_WARN = 0x200,   // Warning.
        MIO_ZSP  = 0x400,   // Zero speed.
        MIO_SPEL = 0x800,   // Soft positive end limit.
        MIO_SMEL = 0x1000,  // Soft negative end limit.
        MIO_TLC  = 0x2000,  // Torque is limited by torque limit value.
        MIO_ABSL = 0x4000,  // Absolute position lost.
        MIO_STA  = 0x8000,  // External start signal.
        MIO_PSD  = 0x10000, // Positive slow down signal
        MIO_MSD  = 0x20000, // Negative slow down signal
    }

    public enum APS_4XMO_Motion_Status
    {
        MTS_CSTP_V  = 0x1,        // Command stop signal.
        MTS_VM_V    = 0x2,        // At maximum velocity.
        MTS_ACC_V   = 0x4,        // In acceleration.
        MTS_DEC_V   = 0x8,        // In deceleration.
        MTS_DIR_V   = 0x10,       // (Last) Moving direction.
        MTS_NSTP_V  = 0x20,       // Normal stop (Motion done).
        MTS_HMV_V   = 0x40,       // In home operation.
        MTS_SMV_V   = 0x80,       // Single axis move (relative, absolute, velocity move).
        MTS_LIP_V   = 0x100,      // Linear interpolation.
        MTS_CIP_V   = 0x200,      // Circular interpolation.
        MTS_VS_V    = 0x400,      // At start velocity.
        MTS_PMV_V   = 0x800,      // Point table move.
        MTS_PDW_V   = 0x1000,     // Point table dwell move.
        MTS_PPS_V   = 0x2000,     // Point table pause state.
        MTS_SLV_V   = 0x4000,     // Slave axis move.
        MTS_JOG_V   = 0x8000,     // Jog move.
        MTS_ASTP_V  = 0x10000,    // Abnormal stop.
        MTS_SVONS_V = 0x20000,    // Servo off stopped.
        MTS_EMGS_V  = 0x40000,    // EMG / SEMG stopped.
        MTS_ALMS_V  = 0x80000,    // Alarm stop.
        MTS_WANS_V  = 0x100000,   // Warning stopped.
        MTS_PELS_V  = 0x200000,   // PEL stopped.
        MTS_MELS_V  = 0x400000,   // MEL stopped.
        MTS_ECES_V  = 0x800000,   // Error counter check level reaches and stopped.
        MTS_SPELS_V = 0x1000000,  // Soft PEL stopped.
        MTS_SMELS_V = 0x2000000,  // Soft MEL stopped.
        MTS_STPOA_V = 0x4000000,  // Stop by others axes.
        MTS_GDCES_V = 0x8000000,  // Gantry deviation error level reaches and stopped.
        MTS_GTM_V   = 0x10000000, // Gantry mode turn on.
        MTS_PAPB_V  = 0x20000000, // Pulsar mode turn on.
    }
}
