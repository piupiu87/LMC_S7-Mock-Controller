using System.Runtime.InteropServices;
using System.Text;

namespace AdlinkMockController
{
    // Consolidated P/Invoke declarations for APS168.dll.
    // CIOAdlink delegates its three mock-helper calls here so all DLL imports live in one place.
    internal static class CAps168
    {
        // ── Servo ─────────────────────────────────────────────────────────────
        [DllImport("APS168.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        internal static extern int APS_set_servo_on(int axisId, int on);

        // ── Position / velocity readback ──────────────────────────────────────
        [DllImport("APS168.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        internal static extern int APS_get_position(int axisId, out int position);

        [DllImport("APS168.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        internal static extern int APS_get_command(int axisId, out int command);

        [DllImport("APS168.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        internal static extern int APS_get_command_velocity(int axisId, out int vel);

        [DllImport("APS168.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        internal static extern int APS_get_error_code(int axisId, int index, out int errorCode);

        // ── Status bitmasks (return value IS the bitmask, no out param) ───────
        [DllImport("APS168.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        internal static extern int APS_motion_status(int axisId);

        [DllImport("APS168.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        internal static extern int APS_motion_io_status(int axisId);

        [DllImport("APS168.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        internal static extern int APS_reset_error(int axisId);

        // ── Motion commands ───────────────────────────────────────────────────
        [DllImport("APS168.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        internal static extern int APS_absolute_move(int axisId, int position, int maxSpeed);

        [DllImport("APS168.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        internal static extern int APS_relative_move(int axisId, int distance, int maxSpeed);

        [DllImport("APS168.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        internal static extern int APS_velocity_move(int axisId, int maxSpeed);

        [DllImport("APS168.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        internal static extern int APS_home_move(int axisId);

        [DllImport("APS168.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        internal static extern int APS_stop_move(int axisId);

        [DllImport("APS168.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        internal static extern int APS_emg_stop(int axisId);

        // ── Jog ───────────────────────────────────────────────────────────────
        [DllImport("APS168.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        internal static extern int APS_jog_start(int axisId, int dir);

        [DllImport("APS168.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        internal static extern int APS_jog_stop(int axisId);

        // ── Mock helpers ──────────────────────────────────────────────────────
        [DllImport("APS168.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true)]
        internal static extern short APS_mock_set_state_path([MarshalAs(UnmanagedType.LPWStr)] string filepath);

        [DllImport("APS168.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true)]
        internal static extern int APS_mock_get_state_path([MarshalAs(UnmanagedType.LPWStr)] StringBuilder buf, int bufSize);

        [DllImport("APS168.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        internal static extern short APS_mock_set_di_bit_modno(short modNo, short chnlNo, bool bOn);
    }
}
