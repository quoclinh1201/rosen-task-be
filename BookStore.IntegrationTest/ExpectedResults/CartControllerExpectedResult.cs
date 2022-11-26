using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.IntegrationTest.ExpectedResults
{
    public class CartControllerExpectedResult
    {
        public string AddToCart_ProductId_ExpectedResult = @"{
          'content': true,
          'error': null,
          'is_success': true
        }";

        public string AddToCart_WrongProductId_ExpectedResult = @"{
          'content': false,
          'error': {
            'code': 400,
            'type': 'Bad Request',
            'message': 'Sản phẩm không tồn tại.'
          },
          'is_success': false
        }";

        public string WrongProductId_ExpectedResult = @"{
          'content': null,
          'error': {
            'code': 400,
            'type': 'Bad Request',
            'message': 'Sản phẩm không tồn tại.'
          },
          'is_success': false
        }";

        public string IncreaseProductQuantity_ProductId_ExpectedResult = @"{
          'content': {
            'cart_items': [
              {
                'product_id': 3,
                'product_name': 'Nhà Giả Kim (Tái Bản 2020)',
                'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_3_1.jpg',
                'quantity': 3,
                'price': '50,000đ',
                'sub_total_price': '150,000đ'
              }
            ],
            'total_price': '150,000đ'
          },
          'error': null,
          'is_success': true
        }";

        public string DecreaseProductQuantity_ProductId_ExpectedResult = @"{
          'content': {
            'cart_items': [
              {
                'product_id': 3,
                'product_name': 'Nhà Giả Kim (Tái Bản 2020)',
                'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_3_1.jpg',
                'quantity': 2,
                'price': '50,000đ',
                'sub_total_price': '100,000đ'
              }
            ],
            'total_price': '100,000đ'
          },
          'error': null,
          'is_success': true
        }";

        public string GetCart_ExpectedResult = @"{
          'content': {
            'cart_items': [
              {
                'product_id': 3,
                'product_name': 'Nhà Giả Kim (Tái Bản 2020)',
                'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_3_1.jpg',
                'quantity': 2,
                'price': '50,000đ',
                'sub_total_price': '100,000đ'
              }
            ],
            'total_price': '100,000đ'
          },
          'error': null,
          'is_success': true
        }";

        public string RemoveProduct_ProductId_ExpectedResult = @"{
          'content': {
            'cart_items': [],
            'total_price': '0đ'
          },
          'error': null,
          'is_success': true
        }";
    }
}
