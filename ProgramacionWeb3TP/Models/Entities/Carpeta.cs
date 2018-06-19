using ProgramacionWeb3TP.Models.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgramacionWeb3TP.Models
{
    [MetadataType(typeof(CarpetaMetadata))]
    public partial class Carpeta
    {
    }
}