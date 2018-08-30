 public Collider2D objectCollider;
 public Collider2D anotherCollider;
 
 private void SomeMethod()
 {
     if (objectCollider.IsTouching(anotherCollider))
     {
        Debug.log("check");
     }
 }