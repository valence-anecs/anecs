# Architecture principles

## Canonical authority boundary

Extraction, ranking and LLM components produce proposals. Only deterministic validators and explicitly authorized policies may create accepted canonical state.

## Identity safety

Source subjects are not ontic entities. Evidence from different source subjects must not be merged without an explicit identity resolution decision and recorded basis.

## Evidence-first epistemic state

Observation, Claim, EvidenceRelation and StateEstimate are separate lifecycle objects. Conflict remains visible; it is not compressed into a single trust score.

## Bitemporal interpretation

Valid time and acquisition time answer different questions. Replay requires both the state that was believed and the evidence available at the decision point.

## Context-bound projection

Supply and Demand are projections under agent, goal, context, policy and snapshot. They are not permanent global profiles.

## Decision-sensitive unknowns

Missing information is prioritized by its ability to change a candidate path, readiness state or terminal decision—not by generic uncertainty.

## Deterministic next action

Policy P0 is simple and inspectable: authorization first; hard blockers before non-blockers; greater decision impact before lower impact; then lower cost and latency; finally stable lexical tie-breakers.

## Measured value

ANECS value is reduced coordination cost under preserved decision quality. Candidate count or ontology coverage alone is not evidence of utility.
