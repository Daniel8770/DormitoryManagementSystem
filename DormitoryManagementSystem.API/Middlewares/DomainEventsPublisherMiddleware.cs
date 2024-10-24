using DormitoryManagementSystem.Infrastructure.Common.DomainEvents;

namespace DormitoryManagementSystem.API.Middlewares;

public class DomainEventsPublisherMiddleware
{
    private DomainEventPublisher domainEventPublisher;
    private RequestDelegate next;

    public DomainEventsPublisherMiddleware(DomainEventPublisher domainEventPublisher, RequestDelegate next)
    {
        this.domainEventPublisher = domainEventPublisher;
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await next(context);
        await domainEventPublisher.PublishAllEventsInEventStore();
    }
}

public static class DomainEventsPublisherMiddlewareExtensions
{
    public static IApplicationBuilder UseDomainEventsPublisher(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<DomainEventsPublisherMiddleware>();
    }
}
