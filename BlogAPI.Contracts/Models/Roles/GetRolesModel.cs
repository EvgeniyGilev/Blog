using BlogAPI.DATA.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Contracts.Models.Roles
{

    public class GetRolesModel
    {
        public int Count    { get; set; }

        public List<ShowRoleView> Roles { get; set; }


    }
}
