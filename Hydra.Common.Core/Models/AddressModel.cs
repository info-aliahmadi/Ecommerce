namespace Hydra.Common.Core.Models
{
    public class AddressModel
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Title { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int UserId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int CountryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string CountryName { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int StateProvinceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string StateProvinceName { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string City { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string County { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Address1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ZipPostalCode { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// GPS location
        /// </summary>
        public string GeoLocation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// default address
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Orders { get; set; }


    }
}