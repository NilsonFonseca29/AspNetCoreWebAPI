using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SmartSchool.WebAPI.Helpers
{
    public class PageList<T> : List<T>
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public PageList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            //AddRange adiciona todos os alunos e não apenas um
            this.AddRange(items); 
        }
        // para trabalhar com os assincronos
        public static async Task<PageList<T>> CreateAsync(
            IQueryable<T> source, int pageNumber, int pageSize) //CreateAsync vai passar como parametro para IQueryable quais itens serão paginados
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber-1)*pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
            return new PageList<T>(items, count, pageNumber, pageSize);
        }
    }
}