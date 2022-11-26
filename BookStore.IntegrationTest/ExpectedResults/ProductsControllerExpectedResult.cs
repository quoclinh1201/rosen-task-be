using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.IntegrationTest.ExpectedResults
{
    public class ProductsControllerExpectedResult
    {
        public string GetListProducts_WithoutParameters_ExpectedResult = @"{
              'current_page': 1,
              'total_pages': 2,
              'page_size': 10,
              'total_count': 12,
              'has_previous': false,
              'has_next': true,
              'content': [
                {
                  'product_id': 1,
                  'product_name': 'Giải Thích Ngữ Pháp Tiếng Anh (Tái Bản 2022)',
                  'price': '50,000đ',
                  'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_1_1.jpg',
                  'is_available': true
                },
                {
                  'product_id': 2,
                  'product_name': 'Sách Tự Học Hiragana Katakana',
                  'price': '50,000đ',
                  'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_2_1.jpg',
                  'is_available': true
                },
                {
                  'product_id': 3,
                  'product_name': 'Nhà Giả Kim (Tái Bản 2020)',
                  'price': '50,000đ',
                  'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_3_1.jpg',
                  'is_available': true
                },
                {
                  'product_id': 4,
                  'product_name': 'Thần Thoại Bắc Âu',
                  'price': '50,000đ',
                  'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_4_1.jpg',
                  'is_available': true
                },
                {
                  'product_id': 5,
                  'product_name': 'Hiểu Về Trái Tim (Tái Bản 2019)',
                  'price': '50,000đ',
                  'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_5_1.jpg',
                  'is_available': true
                },
                {
                  'product_id': 6,
                  'product_name': 'Rèn Luyện Tư Duy Phản Biện',
                  'price': '50,000đ',
                  'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_6_1.jpg',
                  'is_available': true
                },
                {
                  'product_id': 7,
                  'product_name': 'Giải Thích Ngữ Pháp Tiếng Anh (Tái Bản 2022)',
                  'price': '50,000đ',
                  'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_1_1.jpg',
                  'is_available': false
                },
                {
                  'product_id': 8,
                  'product_name': 'Sách Tự Học Hiragana Katakana',
                  'price': '50,000đ',
                  'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_2_1.jpg',
                  'is_available': false
                },
                {
                  'product_id': 9,
                  'product_name': 'Nhà Giả Kim (Tái Bản 2020)',
                  'price': '50,000đ',
                  'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_3_1.jpg',
                  'is_available': true
                },
                {
                  'product_id': 10,
                  'product_name': 'Thần Thoại Bắc Âu',
                  'price': '50,000đ',
                  'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_4_1.jpg',
                  'is_available': true
                }
              ],
              'error': null,
              'is_success': true
            }";

        public string GetListProducts_SearchByNameAndCategory_ExpectedResult = @"{
              'current_page': 1,
              'total_pages': 1,
              'page_size': 10,
              'total_count': 4,
              'has_previous': false,
              'has_next': false,
              'content': [
                {
                  'product_id': 1,
                  'product_name': 'Giải Thích Ngữ Pháp Tiếng Anh (Tái Bản 2022)',
                  'price': '50,000đ',
                  'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_1_1.jpg',
                  'is_available': true
                },
                {
                  'product_id': 2,
                  'product_name': 'Sách Tự Học Hiragana Katakana',
                  'price': '50,000đ',
                  'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_2_1.jpg',
                  'is_available': true
                },
                {
                  'product_id': 7,
                  'product_name': 'Giải Thích Ngữ Pháp Tiếng Anh (Tái Bản 2022)',
                  'price': '50,000đ',
                  'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_1_1.jpg',
                  'is_available': false
                },
                {
                  'product_id': 8,
                  'product_name': 'Sách Tự Học Hiragana Katakana',
                  'price': '50,000đ',
                  'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_2_1.jpg',
                  'is_available': false
                }
              ],
              'error': null,
              'is_success': true
            }";

        public string GetListProducts_SearchByNameButNotMatch_ExpectedResult = @"{
              'current_page': 1,
              'total_pages': 0,
              'page_size': 10,
              'total_count': 0,
              'has_previous': false,
              'has_next': false,
              'content': [],
              'error': null,
              'is_success': true
            }";

        public string GetListProducts_SearchByName_ExpectedResult = @"{
              'current_page': 1,
              'total_pages': 1,
              'page_size': 10,
              'total_count': 2,
              'has_previous': false,
              'has_next': false,
              'content': [
                {
                  'product_id': 4,
                  'product_name': 'Thần Thoại Bắc Âu',
                  'price': '50,000đ',
                  'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_4_1.jpg',
                  'is_available': true
                },
                {
                  'product_id': 10,
                  'product_name': 'Thần Thoại Bắc Âu',
                  'price': '50,000đ',
                  'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_4_1.jpg',
                  'is_available': true
                }
              ],
              'error': null,
              'is_success': true
            }";

        public string GetListProducts_SearchByCategory_ExpectedResult = @"{
              'current_page': 1,
              'total_pages': 1,
              'page_size': 10,
              'total_count': 4,
              'has_previous': false,
              'has_next': false,
              'content': [
                {
                  'product_id': 5,
                  'product_name': 'Hiểu Về Trái Tim (Tái Bản 2019)',
                  'price': '50,000đ',
                  'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_5_1.jpg',
                  'is_available': true
                },
                {
                  'product_id': 6,
                  'product_name': 'Rèn Luyện Tư Duy Phản Biện',
                  'price': '50,000đ',
                  'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_6_1.jpg',
                  'is_available': true
                },
                {
                  'product_id': 11,
                  'product_name': 'Hiểu Về Trái Tim (Tái Bản 2019)',
                  'price': '50,000đ',
                  'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_5_1.jpg',
                  'is_available': false
                },
                {
                  'product_id': 12,
                  'product_name': 'Rèn Luyện Tư Duy Phản Biện',
                  'price': '50,000đ',
                  'product_image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_6_1.jpg',
                  'is_available': true
                }
              ],
              'error': null,
              'is_success': true
            }";

        public string GetDetailProductById_ExpectedResult = @"{
            'content': {
                'product_id': 5,
                'category_id': 3,
                'product_name': 'Hiểu Về Trái Tim (Tái Bản 2019)',
                'product_images': [
                    {
                    'product_image_id': 11,
                    'image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_5_1.jpg'
                    },
                    {
                    'product_image_id': 12,
                    'image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_5_2.jpg'
                    },
                    {
                    'product_image_id': 13,
                    'image_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/p_5_3.jpg'
                    }
                ],
                'category': 'Tâm lý kỹ năng',
                'price': '50,000đ',
                'quantity': 98,
                'supplier_name': 'FIRST NEWS',
                'author': 'Minh Niệm',
                'translator': '',
                'name_of_publisher': 'NXB Tổng Hợp TPHCM',
                'year_of_publication': 2019,
                'weight': 500,
                'size': '13 x 20.5 cm',
                'number_of_pages': 480,
                'description': '“Hiểu về trái tim” là một cuốn sách đặc biệt được viết bởi thiền sư Minh Niệm. Với phong thái và lối hành văn gần gũi với những sinh hoạt của người Việt, thầy Minh Niệm đã thật sự thổi hồn Việt vào cuốn sách nhỏ này.',
                'is_active': true,
                'is_available': true
            },
            'error': null,
            'is_success': true
        }";

        public string GetDetailProductById_WrongId_ExpectedResult = @"{
            'content': null,
            'error': {
            'code': 404,
            'type': 'Not Found',
            'message': 'Sản phẩm không tồn tại.'
            },
            'is_success': false
        }";

    }
}
