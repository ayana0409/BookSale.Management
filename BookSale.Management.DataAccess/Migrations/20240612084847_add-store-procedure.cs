using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSale.Management.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addstoreprocedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            AddGetAllBooksStorePricedure(migrationBuilder);
            AddGetAllOrdersStorePricedure(migrationBuilder);
            AddGetChartOrderStorePricedure(migrationBuilder);
            AddGetReportOrdersStorePricedure(migrationBuilder);

        }
        private void AddGetAllBooksStorePricedure(MigrationBuilder migrationBuilder)
        {
            string query = $@"
					IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'GetBookByPagination')
					BEGIN
						EXEC('CREATE PROCEDURE GetBookByPagination
									@keyword nvarchar(500),
									@pageIndex int,
									@pageSize int,
									@totalRecords int out
								AS
								BEGIN
 
									select g.Name as GenreName, b.Code, b.Title, b.Available, b.Price, b.Publisher, b.Author, b.CreatedOn, b.Id,
											ROW_NUMBER() over(order by b.CreatedOn) as RowNo into #tempBook
									from Book b join Genre g on b.GenreId = g.Id and g.IsActive = 1 and b.IsActive = 1
									where ISNULL(@keyword, '''') = '''' or b.Code like ''%'' +@keyword+ ''%''
																	or b.Title like ''%'' +@keyword+ ''%''
																	or b.Price like ''%'' +@keyword+ ''%''
																	or b.Publisher like ''%'' +@keyword+ ''%''
																	or b.Author like ''%'' +@keyword+ ''%''
																	or g.Name like ''%'' +@keyword+ ''%''

									select @totalRecords = COUNT(*) 
									from #tempBook

									select * 
									from #tempBook
									where RowNo between @pageIndex and @pageSize * (@pageIndex + 1);
								END')
					END
					";

            migrationBuilder.Sql(query, suppressTransaction: true);
        }

        private void AddGetAllOrdersStorePricedure(MigrationBuilder migrationBuilder)
        {
            string query = $@"
					IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'spGetAllOrderByPagination')
					BEGIN
						EXEC('CREATE PROCEDURE spGetAllOrderByPagination
									@keyword nvarchar(500),
									@pageIndex int,
									@pageSize int,
									@totalRecords int out
								AS
								BEGIN
 
									select o.Id, o.Code, o.createdOn, o.PaymentMethod, o.Status, ua.FullName, 
										SUM(od.Quantity * od.UnitPrice) as TotalOrder,
										ROW_NUMBER() OVER(order by o.createdOn DESC) as RowNo
									into #tempOrder
									from [Order] o left join UserAddress ua on ua.Id = o.AddressId
													join OrderDetail od on od.OrderId = o.Id
									where ISNULL(@keyword, '''') = '''' or o.Code like ''%'' +@keyword+ ''%''
																	or ua.FullName like ''%'' +@keyword+ ''%''
									group by o.Id, o.Code, o.createdOn, o.PaymentMethod, o.Status, ua.FullName

									select @totalRecords = COUNT(*) 
									from #tempOrder

									select * 
									from #tempOrder
									where RowNo between @pageIndex and @pageSize * (@pageIndex + 1);
								END
								')
					END
					";

            migrationBuilder.Sql(query, suppressTransaction: true);
        }

        private void AddGetChartOrderStorePricedure(MigrationBuilder migrationBuilder)
        {
            string query = $@"
					IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'spGetChartOrderByGenre')
					BEGIN
						EXEC('CREATE PROCEDURE spGetChartOrderByGenre
								@genreId int
								AS
								BEGIN
									IF @genreId = 0
										BEGIN
											SELECT g.Name as [Name], COUNT(o.Id) as [Value]
											FROM [Order] o JOIN OrderDetail d ON o.Id = d.OrderId
															JOIN Book b on b.Id = d.ProductId
															RIGHT JOIN Genre g on g.Id = b.GenreId
											WHERE g.IsActive = ''True'' AND @genreId = 0 OR g.Id = @genreId
											GROUP BY g.Name
										END
									ELSE
										BEGIN
											SELECT o.Code as [Name], COUNT(d.ProductId) as [Value]
											FROM [Order] o JOIN OrderDetail d ON o.Id = d.OrderId
															JOIN Book b on b.Id = d.ProductId
															RIGHT JOIN Genre g on g.Id = b.GenreId AND g.Id = @genreId
											WHERE g.IsActive = ''True''
											GROUP BY o.Code
										END
								END')
					END
					";

            migrationBuilder.Sql(query, suppressTransaction: true);
        }

        private void AddGetReportOrdersStorePricedure(MigrationBuilder migrationBuilder)
        {
            string query = $@"
					IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'spGetReportOrderByExcel')
					BEGIN
						EXEC('CREATE PROCEDURE spGetReportOrderByExcel
								@from varchar(15),
								@to varchar(15),
								@genreId int,
								@status int
							AS
							BEGIN
								SELECT o.Code, o.CreatedOn, 
										ua.FullName + '' [Phone number:'' + ua.PhoneNumber + '']'' as CustomerName,
										o.status,
										SUM(d.Quantity) as TotalQuantity, 
										SUM(d.Quantity * d.UnitPrice) as TotalPrice
								FROM [Order] o JOIN OrderDetail d ON o.Id = d.OrderId
												JOIN Book b on b.Id = d.ProductId AND (@genreId = 0 OR b.GenreId = @genreId)
												JOIN UserAddress ua ON o.AddressId = ua.Id
								WHERE o.CreatedOn BETWEEN @from AND @to
									AND (@status = 0 OR o.status = @status)
								GROUP BY o.Code, o.CreatedOn, o.status,
										ua.FullName + '' [Phone number:'' + ua.PhoneNumber + '']''
							END')
					END
					";

            migrationBuilder.Sql(query, suppressTransaction: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
