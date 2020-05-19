# Welcome to the SSTS (Secure Scalable ``Token`` Service)

# What is a ``Token``?
## What is a ``Token`` (short version)?
`Token`s can contain anything that can be represented in computing terms.
## What is a ``Token`` (detailed answer)?
A `Token` can be anything that is meaningful to the end user or the system(s) that support the end user(s).
`Token`s are unaware of the systems that recorded them - though they can, of course, contain a trace back to the source system(s) - if the source system leaves that trace within the `Token`.
A `Token` exists in only 1 `Store`.
A `Token` may be copied.
All copies of a `Token` are dated when the copy was made so it is possible to trace which is the source `Token`, assuming all Stores are searched.
A copy of a `Token` may contain a trace back to the originating `Token`, but this is not required.
## What is a Token ``Store``?
A logical container for `Token`s.
A `Store` may be made up of several physical stores. 
## Are ``Token``s a new form of Cryptocurrency?
No.
## What are the key Principles in ``Token``s?
The key principles on `Token`s are:
* Identity
* Authorship
* Immutability
* Independence
* Longevity
## What are the key Principles in ``Store``s?
The key principles on `Store`s are:
* Identity
* Containment
* Ownership
### `Token` Identity
A `Token` itself is uniquely identifiable in the universe. And because a `Token` never changes (see: `Token` Immutability) then a `Token`'s Identity never changes.
The Identity of a `Token` is distinct from the Identity of the owner(s) of that `Token`.
### `Token` Authorship
A `Token` is *not* owned by the person who created it (with a `Command`), the executor of the *Command* is the *Author*.
All rights to the `Token` are managed through the ownership of the `Store` in which the `Token` is placed.
A `Store` will e used to ensure that the reviewer of `Token`s is authorised to do this.
Nothing stops a `Token` that has been Read being cloned into another store, but the original will not be moved. The clone will have a new creation date/time and will have a new Identity (or Id).
### `Token` Immutability
A `Token` is Immutable then the `Store` in which the `Token` exists in (in logical terms) cannot change (contrast: the owner(s) for that `Store` may change).
Quite simply, a `Token` cannot change - this concept is enforced to deliberately support Event Sourcing scenarios. 
### `Token` Independence
A `Token` may - of course contain a reference to another `Token` (only ecause `Token`s can be used to store any digital thing), ut at a technical level, on `Token` can be said to contained (or be contained within) any other `Token`. 
### `Token` Longevity
As mentioned earlier, a `Token` cannot be changed once recorded, which means it cannot be moved to another `Store` - since this would represent a change.
This opens a Question about how the physical storage of a `Token` relates to which `Store` (essentially a logical thing) the `Token` is found in.
Key to understanding how this can be: the `Store` that a `Token` exists in is a Logical construct. At any one time a `Store` can be placed physically in one or more places.
In reality this will be more than one place (so that te Store is not broken or lost in the case of a physical or network failure of a specific place) - this is part of the *Scalable* on the Secure Scalable Token Service (SSTS).
## ``Store`` Identity
A `Store` is uniquely identifiale in the universe.
A `Store` has an Id which is *Immutable*, but the Id of the `Store` is not exposed outside of the `Store`.
A `Store` has a canonical name.
A deployment of the SSTS will ensure that the canonical name of any `Store` it manages is unique witin that deployment. For technical rwasons an SSTS deployment cannot gaurantee that a `Store` of that same name doesn't exist in any other deployment.
## ``Store`` and Containment?
In logic terms all `Token`s are kept in a specific `Token` `Store`. There may be several logical stores of `Token`s, but any 1 `Token` exists in only 1 `Store`.
A `Store` stands indepedently of any other `Store`.
Other systems may group `Store`s together.
### Logical Stores
A Store (for `Token`s) is owned by an organisation or a person.
The Store of `Token`s is owned by one (or more) identities at specific points in time.
### Physical Stores
Where a `Store` is - phyically - may also change over time. A `Store` is expected to exist in more than one place - in terms of the technical stack that manages the Store - but *logically* there is only 1.
## ``Store`` and Ownership?
The Owner of a `Store` at a specific point in time owns all the `Token`s in that store.
## Who can create (Write) `Token`s?
To a *Write* a `Token` the system is said to be applying a *Command*.
Permissions for *Command*s on `Token`s are set at the `Store` level.
A `Store` may permit anonymous `Token` creation.
A `Store` *may* require that an *Identity* is specified during the creation of a `Token`.
A `Store` that requires that an *Identity* is specified during the creation of a `Token` *may* require that identity have specific attributes (eg: carry a specific Organisation component(s)).
Recall: a `Token` is said to be *Authored* by an *Identity* (or Anonymous, if the `Store` permits this).
Who owns that `Token` at any point in time is derived from who owns the `Store` in which that `Token` exists at the time of asking. 
## Who can use (Read) `Token`s?
Two things are required for executing a *Query* (read) for `Token`s:
1) Which `Store`(s) are searched
2) Authorizaton (on a per-`Store` asis) for the *Query*
A *Query* is executed for an Identity, though a `Store` can e set to permit Anonymous Queries at any time.
# What makes this scalable?
## Separating Command (Write) and Query (Read)
### High performance `Token` recording
### Out of band 
# How does the security work?
## Accessing the service
### Writing `Token`s
### Querying `Token`s
## Auditing
All access to the service is recorded in an audit on the SST deployment. All access (*Command* or *Query*) on any `Store` is kept as an Audit within the `Store` itsef.
# Deloying this service
