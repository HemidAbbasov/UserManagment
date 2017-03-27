using LinqToDB.Data;

namespace UserManagmentLib.DataModels
{
    public partial class FirstDB
    {
        partial void InitDataContext()
        {
            // Значение по-умолчанию = OFF (0). http://www.sqlite.org/pragma.html#pragma_foreign_keys
            this.Execute("PRAGMA foreign_keys = ON;");
        }
    }
}