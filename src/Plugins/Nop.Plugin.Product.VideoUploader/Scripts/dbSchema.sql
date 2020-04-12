
CREATE TABLE ProductVideo (
  Id bigint  NOT NULL identity(1,1),
  FromSec int  NULL,
  ToSec int  NULL,
  ProductId int NOT NULL,
  VideoName varchar(200)  NULL,
  FilePath varchar(400)  NULL,
  Linked bit  NOT NULL,
  ShortLink varchar(20)
)

ALTER TABLE ProductVideo ADD CONSTRAINT PK__ProductV__3214EC070010ACFE PRIMARY KEY (Id)

CREATE INDEX IX_PV_ProductId on ProductVideo (ProductId)

GO