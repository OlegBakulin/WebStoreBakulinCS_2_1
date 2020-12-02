using System;
using System.Collections.Generic;
using System.Text;

namespace WebStoreCoreApplication.Domain
{
    public static class WebApiAdress
    {
        public const string EmployeesAdress = "api/employees";
        public const string ProductsAdress = "api/products";

        public const string OrdersAdress = "api/orders";

        public static class Identity
        {
            public const string IdentUserAdress = "api/users";
            public const string IdentRoleAdress = "api/roles";
        }
    }
}
