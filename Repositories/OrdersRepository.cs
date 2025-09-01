using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using HondosOrders.Api.Models;

namespace HondosOrders.Api.Repositories;

public sealed class OrdersRepository : IOrdersRepository
{
    private readonly string _cs;
    public OrdersRepository(IConfiguration cfg) => _cs = cfg.GetConnectionString("Default")!;

    public async Task<IEnumerable<OrderDto>> FindByInvDateAsync(DateTime date, bool useExerciseTable)
    {
        var source = useExerciseTable
            ? "[dbo].[CSt_EShop_VTRACK_OrderData]"   // exercise table
            : "[dbo].[CSt_EShop_VTRACK_OrderData]";   // production view

        var sql = $@"
SELECT
  ADCode AS ad_code,
  OrderDate AS order_date,
  CourierDescr AS courier_descr,
  Ship_Addrs AS ship_addrs,
  Ship_Region AS ship_region,
  Ship_ZipCode AS ship_zip_code,
  Ship_Email AS ship_email,
  Cod AS cod,
  Ship_Mobile AS ship_mobile,
  Order_Comment AS order_comment,
  Reception AS reception,
  CAST(Invoice_ID AS nvarchar(100)) AS invoice_id,
  WebOrderID AS web_order_id,
  Ship_Cost AS ship_cost,
  PaymentCost AS payment_cost,
  Order_Cost AS order_cost,
  OrderWeight AS order_weight,
  bnlid AS bnlid,
  Shipping_Name AS shipping_name,
  Shipping_LastName AS shipping_last_name,
  Voucher AS voucher,
  CourierCode AS couriercode,
  Store_Code AS store_code,
  StoreCompany_Code AS storecompany_code,
  Inv_Date AS inv_date,
  Inv_ADCode AS inv_adcode
FROM {source}
WHERE CONVERT(date, Inv_Date) = @d
ORDER BY Inv_Date, WebOrderID;";

        using var con = new SqlConnection(_cs);
        return await con.QueryAsync<OrderDto>(sql, new { d = date.Date }, commandType: CommandType.Text);
    }
}
