using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.IntegrationTest.ExpectedResults
{
    public class OrdersControllerExpectedResult
    {
        public string GetAllOrders_ExpectedResult = @"{
          'content': [
            {
              'order_id': 3,
              'total_price': '100,000đ',
              'create_date': '13/11/2022 11:21',
              'status': 'Đã đặt'
            },
            {
              'order_id': 4,
              'total_price': '200,000đ',
              'create_date': '13/11/2022 11:21',
              'status': 'Đã đặt'
            }
          ],
          'error': null,
          'is_success': true
        }";

        public string ReOrder_ExpectedResult = @"{
          'content': true,
          'error': null,
          'is_success': true
        }";

        public string CheckOut_ExpectedResult = @"{
          'content': 10,
          'error': null,
          'is_success': true
        }";

        public string GetOrderDetail_OrderId_ExpectedResult = @"{
          'content': {
            'order_id': 10,
            'delivery_informaion': {
              'delivery_id': 2,
              'receiver_name': 'Lâm Khánh Chi',
              'receiver_phone_number': '0987654322',
              'delivery_address': '441 Đ. Lê Văn Việt, Tăng Nhơn Phú A, Quận 9, Thành phố Hồ Chí Minh, Việt Nam'
            },
            'create_date': '26/11/2022 18:01',
            'total_price': '200,000đ',
            'payment_method': 'COD',
            'status': 'Đã đặt',
            'order_detail': [
              {
                'product_id': 2,
                'product_name': 'Sách Tự Học Hiragana Katakana',
                'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_2_1.jpg',
                'quantity': 1,
                'price': '50,000đ'
              },
              {
                'product_id': 3,
                'product_name': 'Nhà Giả Kim (Tái Bản 2020)',
                'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_3_1.jpg',
                'quantity': 1,
                'price': '50,000đ'
              },
              {
                'product_id': 4,
                'product_name': 'Thần Thoại Bắc Âu',
                'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_4_1.jpg',
                'quantity': 1,
                'price': '50,000đ'
              },
              {
                'product_id': 5,
                'product_name': 'Hiểu Về Trái Tim (Tái Bản 2019)',
                'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_5_1.jpg',
                'quantity': 1,
                'price': '50,000đ'
              }
            ]
          },
          'error': null,
          'is_success': true
        }";

        public string CancelOrder_OrderId_ExpectedResult = @"{
          'content': true,
          'error': null,
          'is_success': true
        }";

        public string CheckoutWhenUnavailableProductInCart_ExpectedResult = @"{
          ""content"": 0,
          ""error"": {
            ""code"": 400,
            ""type"": ""Bad Request"",
            ""message"": ""Số lượng trong giỏ hàng đã vượt quá số lượng hàng trong kho.""
          },
          ""is_success"": false
        }";
    }
}
