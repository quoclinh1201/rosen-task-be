using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.IntegrationTest.ExpectedResults
{
    public class UsersControllerExpectedResult
    {
        public string GetOwnProfile_ExpectedResult = @"{
          'content': {
            'full_name': 'Lâm Khánh Chi',
            'email': null,
            'phone_number': '0987654322',
            'avatar_url': 'https://rosentaskstorage.blob.core.windows.net/book-store/avt_2.jpg',
            'gender': true,
            'is_facebook_account' : false
          },
          'error': null,
          'is_success': true
        }";
    }
}
