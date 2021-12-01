using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Kursinis.Models;
using System.Globalization;

namespace Kursinis.Models
{
    public class OrderIndexModel
    {
		[BindNever]
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter your address")]
		[StringLength(100)]
		[Display(Name = "Address")]
		public string Address { get; set; }

		[Required(ErrorMessage = "Please enter your zip code")]
		[Display(Name = "Zip code")]
		[StringLength(10, MinimumLength = 4)]
		public string ZipCode { get; set; }

		[Required(ErrorMessage = "Please enter your country")]
		[StringLength(50)]
		public string Country { get; set; }

		[Required(ErrorMessage = "Please enter your city")]
		[StringLength(50)]
		public string City { get; set; }

		[BindNever]
		[ScaffoldColumn(false)]
		public int OrderTotal { get; set; }

		public string OrderTotalFormat { get => OrderTotal.ToString("c", CultureInfo.CreateSpecificCulture("en-US")); }

		[BindNever]
		[ScaffoldColumn(false)]
		public DateTime OrderPlaced { get; set; }

		public string UserId { get; set; }
		public string UserFullName { get; set; }
		public IEnumerable<OrderDetailListingModel> OrderLines { get; set; }
	}
}
