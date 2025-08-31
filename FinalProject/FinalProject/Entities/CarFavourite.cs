namespace FinalProject.Entities
{
  public class CarFavourite
  {
    public int CarId { get; set; }
    public virtual required Car Car { get; set; }

    public int FavouriteId { get; set; }
    public virtual required Favourites Favourites { get; set; }
  }
}
