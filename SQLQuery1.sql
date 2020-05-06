select * from AspNetUsers

select * from AspNetRoles

select * from AspNetUserRoles
ad6f7ea8-dc1b-4ace-85b5-0c7e6a8e21e1
ad6f7ea8-dc1b-4ace-85b5-0c7e6a8e21e1

select users.Id as userId,users.UserName,roles.Name from AspNetUsers users left join AspNetUserRoles user_role on user_role.UserId=users.Id Left Join  AspNetRoles roles on roles.Id=user_role.RoleId