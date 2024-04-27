using ITM.API.Controllers.V1;

namespace Test.E2E.API.Controllers.V1
{
    class TestBaseController
    {
        class BaseControllerTestWrapper : BaseController
        {
            private readonly ITM.Interfaces.Entities.User currentUser = new ITM.Interfaces.Entities.User { ID = 100001 };

            public override ITM.Interfaces.Entities.User CurrentUser
            {
                get
                {
                    return currentUser;
                }
            }

            public new void SetCreatedModifiedProperties(object obj, string propNameDate, string propNameID)
            {
                base.SetCreatedModifiedProperties(obj, propNameDate, propNameID);
            }

        }
        
    }
}
