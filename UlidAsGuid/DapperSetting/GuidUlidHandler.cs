using System.Data;
using static Dapper.SqlMapper;

namespace UlidAsGuid.DapperSetting;

public class GuidUlidHandler : TypeHandler<Ulid>
{
    public override Ulid Parse(object value)
    {
        return new Ulid((Guid)value);
    }

    public override void SetValue(IDbDataParameter parameter, Ulid value)
    {
        parameter.DbType = DbType.Guid;
        parameter.Size = 16;
        parameter.Value = value.ToGuid();
    }
}

