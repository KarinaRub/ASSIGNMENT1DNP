using System;
using System.Threading.Tasks;
using RepositoryContracts;
using Entities;

namespace CLI.UI.ManagePosts
{
    public class CreatePostView
    {
        private readonly PostInterface postRepository;

        public CreatePostView(PostInterface postRepository)
        {
            this.postRepository = postRepository;
        }
        public async Task ShowAsync()
        {
            Console.Write("Enter post title: ");
            string? title = Console.ReadLine();

            Console.Write("Enter post body: ");
            string? body = Console.ReadLine();

            Console.Write("Enter User ID for this post: ");
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int userId))
            {
                Console.WriteLine("Invalid user ID. Please enter a number.");
                return;
            }

            var post = new Post
            {
                Title = title,
                Body = body,
                UserId = userId
            };

            await postRepository.AddAsync(post);
            Console.WriteLine("Post created successfully!");

        }
    }
}

