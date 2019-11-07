using System;

namespace NBioBSPCOMLib
{
    /// <summary>
    /// Error code of BSP
    /// </summary>
    public class NBioBSPError
    {
        private const int BASE_GENERAL = (0x0000);
        private const int BASE_DEVICE = (0x0100);
        private const int BASE_UI = (0x0200);
        private const int BASE_NSEARCH = (0x0300);

        public const int NONE = (0);
        public const int INVALID_HANDLE = (BASE_GENERAL + 0x01);
        public const int INVALID_POINTER = (BASE_GENERAL + 0x02);
        public const int INVALID_TYPE = (BASE_GENERAL + 0x03);
        public const int FUNCTION_FAIL = (BASE_GENERAL + 0x04);
        public const int STRUCTTYPE_NOT_MATCHED = (BASE_GENERAL + 0x05);
        public const int ALREADY_PROCESSED = (BASE_GENERAL + 0x06);
        public const int EXTRACTION_OPEN_FAIL = (BASE_GENERAL + 0x07);
        public const int VERIFICATION_OPEN_FAIL = (BASE_GENERAL + 0x08);
        public const int DATA_PROCESS_FAIL = (BASE_GENERAL + 0x09);
        public const int MUST_BE_PROCESSED_DATA = (BASE_GENERAL + 0x0a);
        public const int INTERNAL_CHECKSUM_FAIL = (BASE_GENERAL + 0x0b);
        public const int ENCRYPTED_DATA_ERROR = (BASE_GENERAL + 0x0c);
        public const int UNKNOWN_FORMAT = (BASE_GENERAL + 0x0d);
        public const int UNKNOWN_VERSION = (BASE_GENERAL + 0x0e);
        public const int VALIDITY_FAIL = (BASE_GENERAL + 0x0f);

        public const int INIT_MAXFINGER = (BASE_GENERAL + 0x10);
        public const int INIT_SAMPLESPERFINGER = (BASE_GENERAL + 0x11);
        public const int INIT_ENROLLQUALITY = (BASE_GENERAL + 0x12);
        public const int INIT_VERIFYQUALITY = (BASE_GENERAL + 0x13);
        public const int INIT_IDENTIFYQUALITY = (BASE_GENERAL + 0x14);
        public const int INIT_SECURITYLEVEL = (BASE_GENERAL + 0x15);

        public const int INVALID_MINSIZE = (BASE_GENERAL + 0x16);
        public const int INVALID_TEMPLATE = (BASE_GENERAL + 0x17);

        public const int EXPIRED_VERSION = (BASE_GENERAL + 0x18);

        public const int DEVICE_OPEN_FAIL = (BASE_DEVICE + 0x01);
        public const int INVALID_DEVICE_ID = (BASE_DEVICE + 0x02);
        public const int WRONG_DEVICE_ID = (BASE_DEVICE + 0x03);
        public const int DEVICE_ALREADY_OPENED = (BASE_DEVICE + 0x04);
        public const int DEVICE_NOT_OPENED = (BASE_DEVICE + 0x05);
        public const int DEVICE_BRIGHTNESS = (BASE_DEVICE + 0x06);
        public const int DEVICE_CONTRAST = (BASE_DEVICE + 0x07);
        public const int DEVICE_GAIN = (BASE_DEVICE + 0x08);

        public const int USER_CANCEL = (BASE_UI + 0x01);
        public const int USER_BACK = (BASE_UI + 0x02);
        public const int CAPTURE_TIMEOUT = (BASE_UI + 0x03);

        public const int NSEARCH_INIT_MAXCANDIDATE = (BASE_NSEARCH + 0x01);
        public const int NSEARCH_OPEN_FAIL = (BASE_NSEARCH + 0x02);
        public const int NSEARCH_INIT_FAIL = (BASE_NSEARCH + 0x03);
        public const int NSEARCH_MEM_OVERFLOW = (BASE_NSEARCH + 0x04);
        public const int NSEARCH_SAVE_DB = (BASE_NSEARCH + 0x05);
        public const int NSEARCH_LOAD_DB = (BASE_NSEARCH + 0x06);
        public const int NSEARCH_INVALD_TEMPLATE = (BASE_NSEARCH + 0x07);
        public const int NSEARCH_OVER_LIMIT = (BASE_NSEARCH + 0x08);
        public const int NSEARCH_IDENTIFY_FAIL = (BASE_NSEARCH + 0x09);
        public const int NSEARCH_LICENSE_LOAD = (BASE_NSEARCH + 0x0a);
        public const int NSEARCH_LICENSE_KEY = (BASE_NSEARCH + 0x0b);
        public const int NSEARCH_LICENSE_EXPIRED = (BASE_NSEARCH + 0x0c);
        public const int NSEARCH_DUPLICATED_ID = (BASE_NSEARCH + 0x0d);

        public NBioBSPError()
        {
            //
            // TODO: Add any constructor code
            //
        }
    }
}
