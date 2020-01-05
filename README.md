# Validation Framework
_Tham khảo từ [FluentValidation Framework](https://github.com/JeremySkinner/FluentValidation)_

## Giới thiệu
`Validation Framework` là một framework để xác thực dữ liệu nhập vào của người dùng trên những ứng dụng viết bằng ngôn ngữ C#.

`Validation Framework` cung cấp các loại xác thực như:

- Chuỗi được nhập không rỗng.
- Dữ liệu nhập vào khác null.   
- Kiểm tra dữ liệu nhập vào có phải là email hay không.
- Dữ liệu nhập vào có bằng với yêu cầu hay không.
- Dữ liệu nhập vào có khác với yêu cầu hy không.
- Kiểm tra chung các điều kiện.

## Cài đặt
## Sử dụng
1. Tạo ra một lớp lế thừa lớp `AbstractValidator<T>`. Đây là lớp Abstract cung cấp phương thức Validate để validate dữ liệu nhập vào.

```C#
class CustomValidate : AbstractValidation<Customer>
{
    public CustomValidate()
    {
    }
}
```
2. Tạo một đối tượng `CustomValidate` trong chương trình và gọi hàm `Validate` để xác thực dữ liệu nhập vào.
```C#
Customer customer = new Customer();
CustomValidate validate = new CustomValidate();

Console.Write("Input customer's name: ");
customer.Name = Console.ReadLine();
ValidationResult result = validate.Validate(customer);
```
3. Kiểm tra kết quả trả về của `Validate`

Hàm `Validate` trả về một đối tượng kiểu `ValidationResult` bao gồm:
- `bool IsValid`: Kết quả của thao tác xác thực có thành công hay không.
- `IList<ValidationFailure> Errors`: Danh sách các errors trong quá trình xác thực.

Ví dụ ta có thể in lỗi ra màn hình console như sau:
```C#
ValidationResult result = validate.Validate(customer);

foreach (var results in result.Errors)
{
    Console.WriteLine("Property " + results.PropertyName + " failed validation. Error was: " + results.ErrorMessage);
}
```

4. Sử dụng các `validator`

Ở bước một, chúng ta đã khởi tạo một `CustomValidator` để xác thực thông tin của Customer khi nhập vào. Tuy nhiên, chúng ta chưa có một quy tắc nào để xác thực dữ liệu nhập vào cả.

Để xác thực dữ liệu nhập vào, ta sử dụng `RuleFor` trong hàm khởi tạo lớp `CustomValidator`.

Cú pháp:
```C#
RuleFor(<ClassName> => <ClassName>.<Property>).<Validators>;
```

Ví dụ, để kiểm tra tên của đối tượng `Customer` không được rỗng, ta viết mã nguồn như sau:

```C#
class CustomValidate : AbstractValidation<Customer>
{
    public CustomValidate()
    {
        RuleFor(Customer => Customer.Name).NotEmpty();
    }
}
```
Như vậy, nếu nhập vào thông tin tên của đối tượng `Customer` rỗng, `Validator` sẽ thông báo đối tượng không hợp lệ.

5.Tạo thông báo lỗi tùy chỉnh cho `validator`

`Validation Framework` hỗ trợ bạn tạo ra thông báo lỗi tùy chỉnh thông qua `WithMessage`.

Ví dụ, để thông báo lỗi tùy chỉnh cho `validation` ở mục 4, ta điều chỉnh hàm khởi tạo như sau:
```C#
class CustomValidate : AbstractValidation<Customer>
{
    public CustomValidate()
    {
        RuleFor(Customer => Customer.Name).NotEmpty().WithMessage("Customer name must not be empty!");
    }
}
```

6. Kết hợp nhiều quy tắc cùng nhau:

Một thuộc tính có thể có nhiều ràng buộc, vì vậy, bạn có thể kết hợp nhiều quy tắc trong một `validator` bằng cách `chaining` các rule. Ví dụ, bạn cần kiểm tra `not null` cho tên của `Customer`:
```C#
class CustomValidate : AbstractValidation<Customer>
{
    public CustomValidate()
    {
        RuleFor(Customer => Customer.Name).NotEmpty().WithMessage("Customer name must not be empty!").NotNull();
    }
}
```
7. Quy tắc tùy chỉnh (Custom rule)

`Validation Framework` cho phép bạn tạo ra các rules tùy chình dựa theo nhu cầu của mình. Ví dụ, bạn cần kiểm tra dữ liệu nhập vào có độ dài lớn hơn độ dài tối thiểu hay không, ta có thể làm như sau:
```C#
class CustomValidate : AbstractValidation<Customer>
{
    public CustomValidate()
    {
        RuleFor(Customer => Customer.Name).NotEmpty().WithMessage("Customer name must not be empty!").NotNull().Must(Name => Name.Length > 10).WithMessage("Customer name must be more than 10 characters");
    }
}
```
_Lưu ý:_ Với các rule tùy chỉnh, bạn cần dùng `WithMessage` để đặc tả thông báo lỗi cho rule. Nếu không có, thông báo lỗi sẽ rỗng.

9. Tùy chọn ngôn ngữ hiển thị lỗi

`Validation Framework` cho phép bạn tùy chọn ngon ngữ hiển thị lỗi. Hiện tại, framework đang hỗ trợ 02 ngôn ngữ là tiếng Anh "en" và tiếng Việt "vi".

Để sử dụng, trong hàm khởi tạo CustomValidator, ta thêm dòng mã nguồn sau:
```C#
ValidatorOptions.LanguageManager.Culture = new CultureInfo("vi");
```
8. Danh sách các rule mặc định

| Rule | Mô tả |
|------|-------|
| NotNull | Dữ nhiệu nhập vào phải khác `null` |
| NotEmpty | Dữ liệu nhập vào khác rỗng |
| Equal | Dữ liệu nhập vào phải bằng giá trị được cung cấp |
| NotEqual | Dữ liệu nhập vào phải khác giá trị được cung cấp |
| ValidEmail | Dữ liệu nhập vào đúng định dạng email |
