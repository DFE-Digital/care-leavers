**Performance Test Plan: Care Leavers Website**

---

## 1. Objective
To validate that the Care Leavers website can handle expected and peak user loads without performance degradation, while ensuring Azure infrastructure remains cost-effective.

---

## 2. Assumptions
- No historical traffic data is available.
- Based on comparable GovUK services, estimated traffic is 1,000 users/month.
- Peak usage is expected during UK business hours.

### Projected Usage:
- **Normal:** 30 concurrent users (~3 requests/sec)
- **Spike:** 90 concurrent users (~9 requests/sec)
- **Stress:** 150 concurrent users (~15 requests/sec)

---

## 3. User Journeys (Thread Groups)
The test will simulate 7 primary user journeys across the site:
1. Home -> Money & Benifits -> LeavingCareAllowance
2. Home -> All Support -> Housing & Accomadation ->Leaving care Allowance
3. Home -> YourRights -> FormerRelaventChild
4. Home -> Guides -> WhatHappens when you leave care
5. Home -> Helplines
6. USAC page directlly
7. Pathway Plan -> Translate -> Arabic

Each journey will be represented by a separate **Thread Group**.

---

## 4. Test Scenarios

### Scenario 1: Baseline Test
- **Users:** 1 per thread group (7 total threads)
- **Ramp-up:** 7 seconds
- **Loop Count:** 1
- **Goal:** Validate basic functional behavior and response times (< 3 seconds)

### Scenario 2: Normal Load
- **Users:** 30 users total (distributed across thread groups)
- **Ramp-up:** 60 seconds
- **Loop Count:** Based on 5-minute session duration
- **Throughput Goal:** ~3 RPS

### Scenario 3: Spike Load
- **Users:** 90 users total
- **Ramp-up:** 90 seconds
- **Duration:** 10 minutes
- **Throughput Goal:** ~9 RPS

### Scenario 4: Stress Test
- **Users:** 150 users total
- **Ramp-up:** 120 seconds
- **Duration:** 10–15 minutes
- **Goal:** Observe system limits, monitor infrastructure metrics

---

## 5. Configuration Details
- **HTTP Cookie Manager:** Added to each Thread Group with "Clear cookies each iteration" enabled.
- **HTTP Cache Manager:** Added with "Clear cache each iteration" enabled.
- **Timers:** Constant or Gaussian timers to simulate real user think time.
- **Assertions:** Response time assertions (< 3000ms), HTTP status = 200
- **Listeners:** Aggregate Report, Summary Report, Response Time Graph

---

## 6. Success Criteria
- **< 1% error rate**
- **Average response time < 2 seconds**
- **95th percentile < 3 seconds**
- **Consistent throughput without errors**
- **No major infrastructure bottlenecks (CPU, Memory, Redis Cache, Contentful API)**

---

## 7. Tools & Monitoring
- **JMeter** for load generation
- **Azure Monitor / Application Insights** for backend monitoring
- **Contentful Metrics (if available)**
- **Redis Cache Monitoring** (Azure Redis Insights)

---

## 8. Risks & Mitigations
| Risk | Mitigation |
|------|------------|
| Lack of historical data | Start with conservative load assumptions |
| Backend API slowness | Monitor third-party response times |
| Infrastructure limits | Tune Azure scaling rules post-test |

---

## 9. Next Steps
- Prepare .jmx file with 7 thread groups
- Configure ramp-up, user load, and loops per scenario
- Schedule Normal, Spike, and Stress tests
- Review metrics and performance dashboards

---

**Prepared by:** Varsha Krishnamurthy
**Date:**  3/04/25
**Project:** DfE Care Leavers

