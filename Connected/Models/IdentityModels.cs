using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Connected.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public string ProfilePicture { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public interface IAppDataContext
    {
        IDbSet<Comment> Comments { get; set; }
        IDbSet<Friendship> Friendships { get; set; }
        IDbSet<Group> Groups { get; set; }
        IDbSet<GroupMember> GroupMembers { get; set; }
        IDbSet<PostDislike> PostDislikes { get; set; }
        IDbSet<PostLike> PostLikes { get; set; }
        IDbSet<PostShare> PostShares { get; set; }
        IDbSet<Recipe> Recipes { get; set; }
        IDbSet<RecipeComment> RecipeComments { get; set; }
        IDbSet<RecipeDislike> RecipeDislikes { get; set; }
        IDbSet<RecipeLike> RecipeLikes { get; set; }
        IDbSet<RecipeShare> RecipeShares { get; set; }
        IDbSet<ToDoList> ToDoLists { get; set; }
        IDbSet<ToDoListTask> ToDoListTasks { get; set; }
        IDbSet<UserMessage> UserMessages { get; set; }
        IDbSet<UserPost> UserPosts { get; set; }
        int SaveChanges();
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IAppDataContext
    {
        
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<Friendship> Friendships { get; set; }
        public IDbSet<Group> Groups { get; set; }
        public IDbSet<GroupMember> GroupMembers { get; set; }
        public IDbSet<PostDislike> PostDislikes { get; set; }
        public IDbSet<PostLike> PostLikes { get; set; }
        public IDbSet<PostShare> PostShares { get; set; }
        public IDbSet<Recipe> Recipes { get; set; }
        public IDbSet<RecipeComment> RecipeComments { get; set; }
        public IDbSet<RecipeDislike> RecipeDislikes { get; set; }
        public IDbSet<RecipeLike> RecipeLikes { get; set; }
        public IDbSet<RecipeShare> RecipeShares { get; set; }
        public IDbSet<ToDoList> ToDoLists { get; set; }
        public IDbSet<ToDoListTask> ToDoListTasks { get; set; }
        public IDbSet<UserMessage> UserMessages { get; set; }
        public IDbSet<UserPost> UserPosts { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<Connected.ViewModels.UserMessageViewModel> UserMessageViewModels { get; set; }

        //public System.Data.Entity.DbSet<Connected.Models.ApplicationUser> ApplicationUsers { get; set; }

       // public System.Data.Entity.DbSet<Connected.ViewModels.SearchViewModel> SearchViewModels { get; set; }

        //public System.Data.Entity.DbSet<Connected.ViewModels.GroupViewModel> GroupViewModels { get; set; }
    }
}