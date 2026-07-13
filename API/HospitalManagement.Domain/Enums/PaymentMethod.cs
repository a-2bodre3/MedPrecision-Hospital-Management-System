
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Enums
{
    public enum PaymentMethod
    {
        Cash = 1,            // نقداً
        CreditCard = 2,      // بطاقة ائتمان
        Insurance = 3,       // تأمين صحي
        MobilePayment = 4,   // دفع عبر الهاتف المحمول (مثل Apple Pay أو Google Pay)
        BankTransfer = 5     // تحويل بنكي
    }

}
