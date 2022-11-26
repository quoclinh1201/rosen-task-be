using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.IntegrationTest.ExpectedResults
{
    public class DeliveryInformationsControllerExpectedResult
    {
        public string CreateDeliveryInformation_ExpectedResult = @"{
            'content': [
            {
                'delivery_id': 2,
                'receiver_name': 'Lâm Khánh Chi',
                'receiver_phone_number': '0987654322',
                'delivery_address': '441 Đ. Lê Văn Việt, Tăng Nhơn Phú A, Quận 9, Thành phố Hồ Chí Minh, Việt Nam'
            },
            {
                'delivery_id': 5,
                'receiver_name': 'Nguyễn Văn A',
                'receiver_phone_number': '0987654123',
                'delivery_address': '177/4 Linh Trung, Phường Linh Trung, Thủ Đức, Thành phố Hồ Chí Minh'
            }
            ],
            'error': null,
            'is_success': true
        }";

        public string GetAllDeliveryInformation_ExpectedResult = @"{
            'content': [
            {
                'delivery_id': 2,
                'receiver_name': 'Lâm Khánh Chi',
                'receiver_phone_number': '0987654322',
                'delivery_address': '441 Đ. Lê Văn Việt, Tăng Nhơn Phú A, Quận 9, Thành phố Hồ Chí Minh, Việt Nam'
            },
            {
                'delivery_id': 5,
                'receiver_name': 'Nguyễn Văn A',
                'receiver_phone_number': '0987654123',
                'delivery_address': '177/4 Linh Trung, Phường Linh Trung, Thủ Đức, Thành phố Hồ Chí Minh'
            }
            ],
            'error': null,
            'is_success': true
        }";

        public string DeleteDeliveryInformation_Id_ExpectedResult = @"{
          'content': true,
          'error': null,
          'is_success': true
        }";
    }
}
