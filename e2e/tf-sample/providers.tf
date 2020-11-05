terraform {
  required_providers {
    null = {
      version = "=2.1.2"
      source = "localhost:5001/local/null"
    }
  }
  required_version = ">0.13"
}
