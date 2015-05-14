using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Connected.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Connected.Tests
{
    class MockDataContext : IAppDataContext
    {
        /// <summary>
        /// Sets up the fake database.
        /// </summary>
        public MockDataContext()
        {
            ApplicationUsers = new InMemoryDbSet<ApplicationUser>();
            // We're setting our DbSets to be InMemoryDbSets rather than using SQL Server.
            IdentityUsers = new InMemoryDbSet<IdentityUser>();
            Comments = new InMemoryDbSet<Comment>();
            Friendships = new InMemoryDbSet<Friendship>();
            Groups = new InMemoryDbSet<Group>();
            GroupMembers = new InMemoryDbSet<GroupMember>();
            PostDislikes = new InMemoryDbSet<PostDislike>();
            PostLikes = new InMemoryDbSet<PostLike>();
            PostShares = new InMemoryDbSet<PostShare>();
            Recipes = new InMemoryDbSet<Recipe>();
            RecipeComments = new InMemoryDbSet<RecipeComment>();
            RecipeDislikes = new InMemoryDbSet<RecipeDislike>();
            RecipeLikes = new InMemoryDbSet<RecipeLike>();
            RecipeShares = new InMemoryDbSet<RecipeShare>();
            ToDoListTasks = new InMemoryDbSet<ToDoListTask>();
            ToDoLists = new InMemoryDbSet<ToDoList>();
            UserMessages = new InMemoryDbSet<UserMessage>();
            UserPosts = new InMemoryDbSet<UserPost>();

        }

        public IDbSet<ApplicationUser> ApplicationUsers { get; set; }
        public IDbSet<IdentityUser> IdentityUsers { get; set; } 
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
        // TODO: bætið við fleiri færslum hér
        // eftir því sem þeim fjölgar í AppDataContext klasanum ykkar!

        public int SaveChanges()
        {
            // Pretend that each entity gets a database id when we hit save.
            int changes = 0;

            return changes;
        }

        public void Dispose()
        {
            // Do nothing!
        }
    }
}
