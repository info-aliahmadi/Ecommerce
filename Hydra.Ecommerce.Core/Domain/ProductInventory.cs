using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class ProductInventory : BaseEntity<int>
{
    /// <summary>
    /// 
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int? AttributeId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal StockQuantity { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal ReservedQuantity { get; set; }

    /// <summary>
    /// قیمت خرید
    /// </summary>
    public decimal BuyUnitPrice { get; set; }

    /// <summary>
    /// تاریخ ساخت
    /// </summary>
    public DateTime CreatedDatetime { get; set; }

    /// <summary>
    /// تاریخ شروع کسر از موجودی
    /// برای بازگشت کالا اگر تاریخ خرید قبل از این تاریخ بود  کمتر بود باید یک رکورد جدید موجودی ثبت شود
    /// چون برای هر فاکتور خرید باید یک رکورد جدید ثبت شود
    /// در صورت اتمام موجودی این رکورد پاک می شود
    /// </summary>
    public DateTime? StartDatetime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public virtual Product Product { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public virtual ProductAttribute? ProductAttribute { get; set; }
}
