namespace HondosOrders.Api.Models;

public sealed class OrderDto
{
    public string? ad_code { get; set; }
    public DateTime? order_date { get; set; }
    public string? courier_descr { get; set; }
    public string? ship_addrs { get; set; }
    public string? ship_region { get; set; }
    public string? ship_zip_code { get; set; }
    public string? ship_email { get; set; }
    public decimal? cod { get; set; }
    public string? ship_mobile { get; set; }
    public string? order_comment { get; set; }
    public int reception { get; set; }
    public string? invoice_id { get; set; }
    public int? web_order_id { get; set; }
    public decimal? ship_cost { get; set; }
    public decimal? payment_cost { get; set; }
    public decimal? order_cost { get; set; }
    public decimal? order_weight { get; set; }
    public string? bnlid { get; set; }                 // missing field now present
    public string? shipping_name { get; set; }
    public string? shipping_last_name { get; set; }
    public string? voucher { get; set; }
    public string? couriercode { get; set; }
    public string? store_code { get; set; }
    public string? storecompany_code { get; set; }
    public DateTime? inv_date { get; set; }            // filter by this (date-only)
    public string? inv_adcode { get; set; }
}
