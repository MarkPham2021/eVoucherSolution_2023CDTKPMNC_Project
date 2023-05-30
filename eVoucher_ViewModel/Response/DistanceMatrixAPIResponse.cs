using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_ViewModel.Response
{
    public class DistanceMatrixAPIResponse
    {
        public string[] destination_addresses {  get; set; }
        public string[] origin_addresses { get; set; }
        public DistanceMatrixRow[] rows { get; set; }
        public string status { get; set; }
        public string? error_message { get; set; }

    }
    public class TextValueObject
    {
        public string text { get; set; }
        public int value { get; set; }
    }
    public class DistanceMatrixElement
    {
        public string status { get; set; }
        public TextValueObject? distance { get; set; }
        public TextValueObject? duration { get; set; }
        public TextValueObject? duration_in_traffic { get; set; }

    }
    public class DistanceMatrixRow
    {
        public DistanceMatrixElement[] elements { get; set; }
    }
}
