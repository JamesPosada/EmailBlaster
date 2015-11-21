using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrezzorXCrm;
using EmailBlaster;


namespace EmailBlaster.Models
{
    
    public class CrmContactModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string Country { get; set; }
        public int? FrezzorId { get; set; }
        private string CrmId { get; set; }
        public int EnrolleId { get; set; }
        public string EnrollerWebAlias { get; set; }
        public FrxContactType CustomerType {get;set;}
        public MarketingListType FrzMarketingList { get; set; }
        public Guid GetCrmId
        { get
            {
                if (string.IsNullOrEmpty(CrmId))
                {
                    return Guid.Empty;
                }
                return Guid.Parse(CrmId);
            }
        }
        public bool SetCrmId(Guid id)
        {
            if(id == Guid.Empty)
            {
                return false;

            }
            CrmId = id.ToString();
            return true;
        }
        public CrmContactModel()
        {
        }

        public CrmContactModel(FrezzorXCrm.Contact contact)
        {

            FirstName = contact.FirstName;
            LastName = contact.LastName;
            Phone = contact.Address1_Telephone1;
            Phone2 = contact.Address1_Telephone2;
            Phone3 = contact.Address1_Telephone3;
            Country = contact.Address1_Country;
            FrezzorId = FrezzorId;
            SetCrmId(contact.Id);
            Email = contact.EMailAddress1;
            Email2 = contact.EMailAddress2;
            Email3 = contact.EMailAddress3;
            EnrolleId = EnrolleId;
            EnrollerWebAlias = EnrollerWebAlias;
            FrzMarketingList = (contact.new_MarketingLists.HasValue) ? (MarketingListType)contact.new_MarketingLists : MarketingListType.DoNotSend;
            CustomerType = (contact.New_Type.HasValue) ? (FrxContactType)contact.New_Type.Value : FrxContactType.Prospect;
        }

    }
}
