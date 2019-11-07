using System;

namespace NBioBSPCOMLib
{
    /// <summary>
    /// Summary description for NBioBSPType.
    /// </summary>

    using UINT8 = System.Byte;
    using UINT16 = System.UInt16;
    using UINT32 = System.UInt32;
    using SINT32 = System.Int32;

    // FIR_DATA
    using FIR_DATA = System.Byte;

    // HANDLE
    using HANDLE = System.UInt32;

    // HWND
    using HWND = System.UInt32;


    public class NBioBSPType
    {
        /* TRUE / FALSE */
        public const int FALSE = (0);
        public const int TRUE = (1);

        /* HANDLE */
        public const int INVALID_HANDLE = (0);

        /* DEVICE_ID - high one byte : device instance, low one byte : device name */
        public class DEVICE_ID
        {
            public const short NONE = (0x0000);
            public const short AUTO = (0x00ff);
        }

        /* DEVICE_NAME */
        public class DEVICE_NAME
        {
            public const byte FDP02 = (0x01);
            public const byte FDU01 = (0x02);
            public const byte OSU02 = (0x03);
            public const byte FDU11 = (0x04);
            public const byte FSC01 = (0x05);
            public const byte FDU03 = (0x06);
        }

        /* MAX_DEVICE */
        public const int MAX_DEVICE_NUM = (0xfe);

        /* TIMEOUT */
        public class TIMEOUT
        {
            public const int INFINITE = (0);
            public const int DEFAULT = (-1);
        }

        /* WINDOW_STYLE */
        public class WINDOW_STYLE
        {
            public const int POPUP = (0);
            public const int INVISIBLE = (1);   // only for Capture()
            public const int CONTINUOUS = (2);

            /* OR flag used only in high 2 bytes. */
            public const int NO_FPIMG = (0x00010000);   // Or flag 
            public const int TOPMOST = (0x00020000);   // Or flag : default flag and not used after v2.3 (WinCE v1.2)
            public const int NO_WELCOME = (0x00040000);   // Or flag : only for enroll 
            public const int NO_TOPMOST = (0x00080000);   // Or flag : additional flag after v2.3 (WinCE v1.2) 
        }

        // FIR_DATA_TYPE
        public enum FIR_DATA_TYPE
        {
            RAW = (0x00),
            INTERMEDIATE = (0x01),
            PROCESSED = (0x02),
            ENCRYPTED = (0x10)
        }

        // FIR_PURPOSE
        public enum FIR_PURPOSE
        {
            VERIFY = (0x01),
            DENTIFY = (0x02),
            ENROLL = (0x03),
            ENROLL_FOR_VERIFICATION_ONLY = (0x04),
            ENROLL_FOR_IDENTIFICATION_ONLY = (0x05),
            AUDIT = (0x06),
            UPDATE = (0x10)
        }

        // INPUT_FIR_FORMAT
        public enum FIR_FORMAT
        {
            STANDARD = (1),
            NBAS = (2)
        }

        // FIR_SECURITY_LEVEL
        public class FIR_SECURITY_LEVEL
        {
            public const int LOWEST = (1);
            public const int LOWER = (2);
            public const int LOW = (3);
            public const int BELOW_NORMAL = (4);
            public const int NORMAL = (5);
            public const int ABOVE_NORMAL = (6);
            public const int HIGH = (7);
            public const int HIGHER = (8);
            public const int HIGHEST = (9);
        }

        // INPUT_FIR_FORM
        public enum INPUT_FIR_FORM
        {
            HANDLE = (0x02),
            FULLFIR = (0x03),
            TEXTENCODE = (0x04),
        }

        // MINCONV_DATA_TYPE for ConvertMinDataToFIRHandle()
        public enum MINCONV_DATA_TYPE
        {
            MINCONV_TYPE_FDP = 0,
            MINCONV_TYPE_FDU,
            MINCONV_TYPE_FDA,
            MINCONV_TYPE_OLD_FDA,
            MINCONV_TYPE_FDAC,

            MINCONV_TYPE_MAX
        };

        // FINGER_ID
        public enum FINGER_ID
        {
            UNKNOWN = (0), // for verify
            RIGHT_THUMB = (1),
            RIGHT_INDEX = (2),
            RIGHT_MIDDLE = (3),
            RIGHT_RING = (4),
            RIGHT_LITTLE = (5),
            LEFT_THUMB = (6),
            LEFT_INDEX = (7),
            LEFT_MIDDLE = (8),
            LEFT_RING = (9),
            LEFT_LITTLE = (10),
        }

        // IMG_TYPE : Constant for FP Image 
        public enum IMG_TYPE
        {
            RAW = (1),
            BMP = (2),
            JPG = (3)
        }

        public NBioBSPType()
        {
            //
            // TODO: Add any constructor code
            //
        }
    }
}
