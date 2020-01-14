(DDD) Domain Events with MediatR

=======





- Add dependency to constructor of DbContext


  	private readonly IDomainEventDispatcher _dispatcher;

        public DbSet<BacklogItem> BacklogItems { get; set; }
        public DbSet<Sprint> Sprints { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
         IDomainEventDispatcher dispatcher)
         : base(options)
        {
            _dispatcher = dispatcher;
        }

- Dispatch domain events in DbContext

	public override int SaveChanges()
        
	{	
            
 	_preSaveChanges().GetAwaiter().GetResult();
            
 	var res = base.SaveChanges();
           
 	return res;

	}

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
 
	{

            await _preSaveChanges();

            var res = await base.SaveChangesAsync(cancellationToken);

            return res;
        }

        private async Task _preSaveChanges()

        {

            await _dispatchDomainEvents();

        }

        /// <summary>

        /// Domain events run within the transaction and
 
        /// allow aggregates to locally communicate with eachother

        /// cleanly

        /// </summary>

        private async Task _dispatchDomainEvents()

        {

            var domainEventEntities = ChangeTracker.Entries<IEntity>()

               .Select(po => po.Entity)

               .Where(po => po.DomainEvents.Any())

               .ToArray();


            foreach (var entity in domainEventEntities)

            {

                IDomainEvent dev;

                while (entity.DomainEvents.TryTake(out dev))
                    await _dispatcher.Dispatch(dev);
            }

        }


- Move dispatcher to your Application Layer

- Setup Dispatcher in Container

 services.AddMediatR(typeof(MediatrDomainEventDispatcher).GetTypeInfo().Assembly);
 services.AddTransient<IDomainEventDispatcher, MediatrDomainEventDispatcher>();
