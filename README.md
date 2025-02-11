# SmsRateLimiter
The Challenge
Your team is working on a critical system responsible for sending SMS messages from
businesses to customers across various applications. At the heart of this system is a method
that interacts with an external messaging provider to deliver these SMS messages. However,
the messaging provider enforces strict limits on how many messages can be sent from business
phone numbers and how many messages can be sent across your entire account per second.
The current setup has led to issues. When these limits are exceeded, the provider rejects the
message, but your team still incurs the cost of calling the provider’s API. To avoid unnecessary
costs, your team needs an internal system that ensures these limits are respected before an
SMS is sent.

Your Task
Your task is to create a microservice in .NET Core (C#) that acts as a gatekeeper, deciding
whether a message can be sent from a given business phone number before calling the
external provider. This microservice will be called from various applications and services across
your infrastructure.
The microservice must answer one critical question:
● Can this message be sent from a given business phone number without exceeding the
provider’s limits?
The provider enforces two specific limits:
● A maximum number of messages can be sent from a single business phone number
per second.
● A maximum number of messages can be sent across the entire account per second.
You need to ensure that your microservice respects these limits in real-time. If either limit is
exceeded, the service should indicate that the message cannot be sent, avoiding the
unnecessary API call.
Performance Expectation
The microservice will need to handle a high volume of calls from different applications across
your system. Your solution should efficiently manage these requests while ensuring it performs
reliably under load and respects the imposed limits.
In addition to managing these limits, your solution should also handle resource management:

● Ensure that resources (e.g., tracking for specific business phone numbers) are not kept
indefinitely for numbers that are no longer active or haven't been used for a period of
time.
Deliverables
● A functioning .NET Core (C#) microservice that provides a way to check whether a
message can be sent without exceeding the provider’s limits.
● Tests that demonstrate how the service behaves under various conditions, including
situations where limits are approached or exceeded.
Extra (Bonus Points)
● Web Interface (Angular or any javascript framework) for monitoring the service
○ Two sections - per account and per number
○ Should show how many messages/second are being processed
○ Filtering per number and date/time range
