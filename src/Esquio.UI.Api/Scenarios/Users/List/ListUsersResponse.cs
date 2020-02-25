﻿using System.Collections.Generic;

namespace Esquio.UI.Api.Scenarios.Users.List
{
    public class ListUsersResponse
    {
        public int Total { get; set; }

        public int PageIndex { get; set; }

        public int Count { get; set; }

        public List<ListUsersResponseDetail> Result { get; set; }
    }

    public class ListUsersResponseDetail
    {
        public string SubjectId { get; set; }

        public string ActAs { get; set; }
    }
}
