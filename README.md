# Scalable E-commerce Platform — Microservices Architecture

## Overview

This project is a study-oriented e-commerce platform designed to explore **microservices architecture**, **event-driven communication**, and **scalable backend design**.

The focus of this repository is not feature completeness, but **architectural clarity and decision-making**. Every component and technology choice was made to reflect real-world backend trade-offs commonly discussed in technical interviews.

---

## Architectural Approach

The system is structured around **bounded contexts**, where each business domain is implemented as an independent microservice.  
Each service owns its logic, its data, and its lifecycle, enabling independent evolution and scalability.

All external traffic enters the system through a dedicated **API Gateway**, which centralizes cross-cutting concerns such as routing, authentication, and rate limiting. This avoids duplicating infrastructure logic across services and keeps domain services focused on business rules.

---

## Why an API Gateway

The API Gateway acts as the single entry point to the platform.  
It was chosen to prevent tight coupling between the frontend and internal services, and to centralize concerns that do not belong to business domains.

This design allows:
- Internal services to remain unaware of clients
- API versioning and routing to evolve independently
- Security and observability to be enforced consistently

The gateway is intentionally kept thin and contains **no business logic**.

---

## Service Communication Strategy

The platform uses a **hybrid communication model**.

Synchronous HTTP communication is used for simple request/response operations such as queries and validations.  
Asynchronous communication via **Kafka** is used for business workflows that involve multiple services, such as order processing and payment handling.

This approach reduces coupling between services and improves resilience, allowing failures or slowdowns in one service to not cascade through the system.

---

## Event-Driven Design

Business workflows are modeled as **domain events** that represent facts which have already occurred.

For example, when an order is created, the Order Service emits an `OrderCreated` event. Other services, such as Payment or Notification, react to this event without direct knowledge of the Order Service.

This design was chosen to:
- Decouple services
- Enable independent scaling
- Avoid synchronous chains between critical domains

---

## Data Ownership and Persistence

Each microservice owns its own database.  
This decision prevents hidden coupling at the data layer and allows each service to evolve its schema independently.

Different databases are used based on domain needs:
- Relational databases are used where strong consistency and transactional integrity are required
- Document databases are used where schema flexibility and read-heavy access patterns are more appropriate

This **polyglot persistence** approach reflects real-world system design rather than enforcing a single database for all problems.

---

## Caching Strategy

Redis is used as an infrastructure component to improve performance and protect critical services.

It is applied selectively for:
- Read-heavy data such as product catalog queries
- Rate limiting at the API Gateway
- Volatile or non-critical data where strong consistency is not required

Caching follows a simple cache-aside pattern to keep consistency manageable.

---

## Repository Organization

This repository uses a **monorepo** structure.  
The goal is to make architectural relationships explicit and simplify CI/CD and refactoring in a single-developer context.

Each service remains logically independent, with its own Docker image and internal structure, while sharing a unified pipeline and infrastructure configuration.

---

## CI/CD Philosophy

GitHub Actions is used to automate build and test processes.

The CI pipeline validates that all services compile and pass tests on every change, reinforcing system stability without adding unnecessary deployment complexity at this stage.

---

## Purpose

This project exists to demonstrate:
- Architectural reasoning
- Understanding of distributed systems trade-offs
- Practical application of microservices principles

It is intentionally designed to support **technical interviews and architectural discussions**, where clarity of thought and justification of decisions matter more than implementation details.
