using DigichList.Core.Entities;
using DigichList.Infrastructure.Data;
using DigichList.Infrastructure.Extensions;
using DigichList.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DigichList.Infrastructure.Seeders
{
    public class ContextSeed
    {
        private readonly DigichListContext _context;
        private readonly RoleRepository _roleRepository;
        private readonly DefectImageRepository _defectImageRepository;
        private readonly UserRepository _userRepository;
        private readonly DefectRepository _defectRepository;
        public ContextSeed()
        {
            _context = new DigichListContext();
            _roleRepository = new RoleRepository(_context);
            _defectImageRepository = new DefectImageRepository();
            _userRepository = new UserRepository(_context);
            _defectRepository = new DefectRepository(_context);
        }

        public async Task SeedRoles()
        {
            if(!_context.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role
                    {
                        Name = "Maid",
                        CanPublishDefects = true
                    },
                    new Role
                    {
                        Name = "Technician",
                        CanPublishDefects = true,
                        CanFixDefects = true
                    },
                };

                await _context.Roles.AddRangeAsync(roles);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedUsers()
        {
            if(!_context.Users.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        FirstName = "Serge",
                        TelegramId = 750882579,
                        Role = await _roleRepository.GetRoleByNameAsync("Maid")
                    },
                    new User
                    {
                        FirstName = "Vasyl",
                        TelegramId = 683684349,
                        Role = await _roleRepository.GetRoleByNameAsync("Technician")
                    },
                    new User
                    {
                        FirstName = "Ivan",
                        LastName = "Demianchuk",
                        TelegramId = 554664751,
                        Role = await _roleRepository.GetRoleByNameAsync("Technician")
                    },
                };

                await _context.Users.AddRangeAsync(users);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedAdmins()
        {
            var admins = new List<Admin>
            {
                new Admin
                {
                    Username = "Serge",
                    Email = "serge@gmail.com",
                    AccessLevel = Admin.AccessLevels.Admin,
                    Password = BCrypt.Net.BCrypt.HashPassword("123123123")
                },
                new Admin
                {
                    Username = "Vasyl",
                    Email = "vasyl@gmail.com",
                    AccessLevel = Admin.AccessLevels.SuperAdmin,
                    Password = BCrypt.Net.BCrypt.HashPassword("234234234")
                },
                new Admin
                {
                    Username = "Ivan",
                    Email = "ivan@gmail.com",
                    AccessLevel = Admin.AccessLevels.Admin,
                    Password = BCrypt.Net.BCrypt.HashPassword("345345345")
                }
            };

            await _context.Admins.AddRangeAsync(admins);
            await _context.SaveChangesAsync();
        }

        public async Task SeedDefects()
        {
            if (!_context.Defects.Any())
            {
                #region Defects List
                var defects = new List<Defect>
                {
                    new Defect
                    {
                        RoomNumber = 12,
                        Description = "Дверцята від тумбочки не закриваються до кінця.",
                        Publisher = await _userRepository.GetUserByTelegramIdAsync(750882579)
                    },
                    new Defect
                    {
                        RoomNumber = 13,
                        Description = "Поломився тримач шлангу в душовій кабіні.",
                        Publisher = await _userRepository.GetUserByTelegramIdAsync(750882579)
                    },
                    new Defect
                    {
                        RoomNumber = 43,
                        Description = "Поломився ламель.",
                        Publisher = await _userRepository.GetUserByTelegramIdAsync(750882579)
                    },
                    new Defect
                    {
                        RoomNumber = 17,
                        Description = "ПРОПАЛИ ШПАЛЕРИ У НОМЕРІ!!!",
                        Publisher = await _userRepository.GetUserByTelegramIdAsync(554664751)
                    },
                    new Defect
                    {
                        RoomNumber = 22,
                        Description = "Протікає кран.",
                        Publisher = await _userRepository.GetUserByTelegramIdAsync(554664751)
                    },
                    new Defect
                    {
                        RoomNumber = 25,
                        Description = "Тріщина у ванній кімнаті",
                        Publisher = await _userRepository.GetUserByTelegramIdAsync(683684349)
                    },
                    new Defect
                    {
                        RoomNumber = 30,
                        Description = "Пропав тримач мила у душовій кабіні.",
                        Publisher = await _userRepository.GetUserByTelegramIdAsync(683684349)
                    },

                };
                #endregion

                await _context.Defects.AddRangeAsync(defects);
                await _context.SaveChangesAsync();



            }
        }

        public async Task SeedAdmins()
        {
            var admins = new List<Admin>
            {
                new Admin
                {
                    Username = "Serge",
                    Email = "serge@gmail.com",
                    AccessLevel = Admin.AccessLevels.Admin,
                    Password = BCrypt.Net.BCrypt.HashPassword("123123123")
                },
                new Admin
                {
                    Username = "Vasyl",
                    Email = "vasyl@gmail.com",
                    AccessLevel = Admin.AccessLevels.SuperAdmin,
                    Password = BCrypt.Net.BCrypt.HashPassword("234234234")
                },
                new Admin
                {
                    Username = "Ivan",
                    Email = "ivan@gmail.com",
                    AccessLevel = Admin.AccessLevels.Admin,
                    Password = BCrypt.Net.BCrypt.HashPassword("345345345")
                }
            };

            await _context.Admins.AddRangeAsync(admins);
            await _context.SaveChangesAsync();
        }

        public async Task SeedDefectImages()
        {
            if (!_context.DefectImages.Any())
            {
                var defects = await _defectRepository.GetAllAsync();
                var images = Directory.GetFiles(@"D:\дипломна\DigichList.Data\Seeders\Images\", "*.jpg");
                var counter = 0;
                foreach (var i in images)
                {
                    var defectImage = await _defectImageRepository.SaveImageAsStringByteArray(i);
                    defectImage.Defect = defects[counter++];
                    _context.DefectImages.Add(defectImage);
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedAssignedDefects()
        {
            if (!_context.AssignedDefects.Any())
            {
                var defects = await _defectRepository.GetAllAsync();
                var technicians = _context.Users.GetTechnicians().ToList();
                Random rnd = new Random();
                int randomTechnician = rnd.Next(technicians.Count);

                foreach (var d in defects)
                {
                    var assignedDefect = new AssignedDefect
                    {
                        Defect = d,
                        AssignedWorker = technicians[randomTechnician]
                    };
                    _context.AssignedDefects.Add(assignedDefect);
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedDatabase()
        {
            await SeedRoles();
            await SeedAdmins();
            await SeedUsers();
            await SeedDefects();
            await SeedDefectImages();
            await SeedAssignedDefects();
        }
    }
}
