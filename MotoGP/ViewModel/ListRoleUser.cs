﻿namespace MotoGP.ViewModel
{
    public class ListRoleUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; } // Danh sách tên quyền
    }
}
