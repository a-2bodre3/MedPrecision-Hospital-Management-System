export function toFormData(obj: any, form?: FormData, namespace?: string): FormData {
  const fd = form || new FormData();
  let formKey: string;

  for (const property in obj) {
    if (obj.hasOwnProperty(property)) {
      if (namespace) {
        // لتجهيز مفاتيح مثل: Address.Street أو Address.City
        formKey = `${namespace}.${property}`;
      } else {
        formKey = property;
      }

      const value = obj[property];

      if (value instanceof File) {
        // لو الحقل ملف أو صورة
        fd.append(formKey, value);
      } else if (value instanceof Array) {
        // لو الحقل مصفوفة
        value.forEach((item, index) => {
          if (typeof item === 'object' && !(item instanceof File)) {
            toFormData(item, fd, `${formKey}[${index}]`);
          } else {
            fd.append(`${formKey}[${index}]`, item);
          }
        });
      } else if (typeof value === 'object' && value !== null && !(value instanceof Date)) {
        // لو الحقل كائن متداخل (مثل الـ Address)
        toFormData(value, fd, formKey);
      } else if (value !== undefined && value !== null) {
        // القيم العادية (string, number, boolean)
        fd.append(formKey, value.toString());
      }
    }
  }

  return fd;
}
