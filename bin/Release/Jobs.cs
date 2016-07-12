using System;
using AmosG.BusinessTier.BusinessEntities;
using Constants = DOORman.BE.Constants;

namespace DOORman.BE
{
    public class Jobs : BusinessEntity
    {
        private Int32 _JobID;
        public Int32 JobID
        {
            get { return _JobID; }
            set { _JobID = value; }
        }

        private String _JobNum;
        public String JobNum
        {
            get { return _JobNum; }
            set { _JobNum = value; }
        }

        private Boolean _BnDJob;
        public Boolean BnDJob
        {
            get { return _BnDJob; }
            set { _BnDJob = value; }
        }

        private String _VNumber;
        public String VNumber
        {
            get { return _VNumber; }
            set { _VNumber = value; }
        }

        private String _QNumber;
        public String QNumber
        {
            get { return _QNumber; }
            set { _QNumber = value; }
        }

        private String _BuilderCode;
        public String BuilderCode
        {
            get { return _BuilderCode; }
            set { _BuilderCode = value; }
        }

        private String _SalesOrderNum;
        public String SalesOrderNum
        {
            get { return _SalesOrderNum; }
            set { _SalesOrderNum = value; }
        }

        private String _CustomerName;
        public String CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }

        private String _SiteAddress;
        public String SiteAddress
        {
            get { return _SiteAddress; }
            set { _SiteAddress = value; }
        }

        private String _Suburb;
        public String Suburb
        {
            get { return _Suburb; }
            set { _Suburb = value; }
        }

        private Short _NumberOfDoors;
        public Short NumberOfDoors
        {
            get { return _NumberOfDoors; }
            set { _NumberOfDoors = value; }
        }

        private DateTime _DateOrdered;
        public DateTime DateOrdered
        {
            get { return _DateOrdered; }
            set { _DateOrdered = value; }
        }

        private DateTime _DateExpected;
        public DateTime DateExpected
        {
            get { return _DateExpected; }
            set { _DateExpected = value; }
        }

        private DateTime _DateArrived;
        public DateTime DateArrived
        {
            get { return _DateArrived; }
            set { _DateArrived = value; }
        }

        private DateTime _DateInstallRequired;
        public DateTime DateInstallRequired
        {
            get { return _DateInstallRequired; }
            set { _DateInstallRequired = value; }
        }

        private DateTime _DateActualInstall;
        public DateTime DateActualInstall
        {
            get { return _DateActualInstall; }
            set { _DateActualInstall = value; }
        }

        private String _SAPNumber;
        public String SAPNumber
        {
            get { return _SAPNumber; }
            set { _SAPNumber = value; }
        }

        private String _ChangeRequired;
        public String ChangeRequired
        {
            get { return _ChangeRequired; }
            set { _ChangeRequired = value; }
        }

        private String _SentTo;
        public String SentTo
        {
            get { return _SentTo; }
            set { _SentTo = value; }
        }

        private DateTime _DateSent;
        public DateTime DateSent
        {
            get { return _DateSent; }
            set { _DateSent = value; }
        }

        private DateTime _DateReturned;
        public DateTime DateReturned
        {
            get { return _DateReturned; }
            set { _DateReturned = value; }
        }

        private DateTime _DateTransmitterReturned;
        public DateTime DateTransmitterReturned
        {
            get { return _DateTransmitterReturned; }
            set { _DateTransmitterReturned = value; }
        }

        private String _Notes;
        public String Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
        }

        private String _SafetyNotes;
        public String SafetyNotes
        {
            get { return _SafetyNotes; }
            set { _SafetyNotes = value; }
        }

        private Int32 _CreatedBy;
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        private Int32 _Charge;
        public Int32 Charge
        {
            get { return _Charge; }
            set { _Charge = value; }
        }

        private String _UBDMap;
        public String UBDMap
        {
            get { return _UBDMap; }
            set { _UBDMap = value; }
        }

        private String _UBDRef;
        public String UBDRef
        {
            get { return _UBDRef; }
            set { _UBDRef = value; }
        }

        private String _UpdateUser;
        public String UpdateUser
        {
            get { return _UpdateUser; }
            set { _UpdateUser = value; }
        }

        private DateTime _Updated;
        public DateTime Updated
        {
            get { return _Updated; }
            set { _Updated = value; }
        }

    }
}
