using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace WebStoreCoreApplication.Domain.DTO.Identity
{
    public class ClaimDTO : UserDTO
    {
        public IEnumerable<Claim> Claim { get; set; }
    }

    public class AddClaimDTO : ClaimDTO { }

    public class RemoveClaimDTO : ClaimDTO { }

    public class ReplaceClaimDTO : UserDTO
    {
        public Claim Claim { get; set; }

        public Claim NewClaim { get; set; }
    }
}
