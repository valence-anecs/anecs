from __future__ import annotations

import unittest

from anecs_lab import canonical_json, sha256_id


class CanonicalJsonTests(unittest.TestCase):
    def test_property_order_does_not_change_output(self) -> None:
        first = {"predicate": "capability:cnc", "subject": "supplier:a"}
        second = {"subject": "supplier:a", "predicate": "capability:cnc"}

        self.assertEqual(canonical_json(first), canonical_json(second))
        self.assertEqual(sha256_id("observation", first), sha256_id("observation", second))


if __name__ == "__main__":
    unittest.main()
