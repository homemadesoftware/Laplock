using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeMadeSoftware.Laplock
{
    internal class PowerEventHandler
    {
        private const int WM_POWERBROADCAST = 0x0218;
        private const int SM_REMOTESESSION = 0x1000;
        private static IntPtr PBT_POWERSETTINGCHANGE = (IntPtr)0x8013;
        private static Guid GUID_MONITOR_POWER_ON = new Guid("02731015-4510-4526-99E6-E5A17EBD1AEA");
        private static Guid GUID_LIDSWITCH_STATE_CHANGE = new Guid(0xBA3E0F4D, 0xB817, 0x4094, 0xA2, 0xD1, 0xD5, 0x63, 0x79, 0xE6, 0xA0, 0xF3);

        [DllImport("User32.dll")]
        private static extern int GetSystemMetrics(int metricType);

        [DllImport("User32.dll")]
        private static extern void LockWorkstation();

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct POWERBROADCAST_SETTING
        {
           public Guid PowerSetting;
           public UInt32 DataLength;
           public byte Data;
        }

        static public void HandleWindowProcCall(Message m)
        {
            if (m.Msg == WM_POWERBROADCAST && m.WParam == PBT_POWERSETTINGCHANGE && GetSystemMetrics(SM_REMOTESESSION) == 0)
            {
                var powerBroadcastSetting = (POWERBROADCAST_SETTING)Marshal.PtrToStructure(m.LParam, typeof(POWERBROADCAST_SETTING));
                if (powerBroadcastSetting.PowerSetting == GUID_MONITOR_POWER_ON || powerBroadcastSetting.PowerSetting == GUID_LIDSWITCH_STATE_CHANGE)
                {
                    // Is the lid open?
                    if (powerBroadcastSetting.Data != 0)
                    {
                        return;
                    }
                    
                    // Lid closed
                    LockWorkstation();
                }
            }
        }
    }
}
