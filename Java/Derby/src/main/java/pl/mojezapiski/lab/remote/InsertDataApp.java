package pl.mojezapiski.lab.remote;

import java.sql.*;

public class InsertDataApp {

    public InsertDataApp() {
    }

    public void insertData() {
        Connection con = null;
        try {
            // Ładujemy plik z klasą sterownika bazy danych
            Class.forName("org.apache.derby.jdbc.ClientDriver");
            // Tworzymy połączenie do bazy danych
            con = DriverManager.getConnection("jdbc:derby://localhost:1527/lab", "lab", "lab");
            // Tworzymy obiekt wyrażenia
            ///Statement statement = con.createStatement();
            Statement statement = con.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_UPDATABLE);
            ///PreparedStatement prst = con.prepareStatement("INSERT INTO Dane VALUES (?, ?, ?, ?)");
            ResultSet rs = statement.executeQuery("SELECT * FROM Dane");
            // Wypełniamy wiersze tabeli Data
            ///statement.executeUpdate("INSERT INTO Dane VALUES (1, 'Nowak', 'Jan', 5.0)");
            ///statement.executeUpdate("INSERT INTO Dane VALUES (2, 'Kowalski', 'Wojciech', 3.0)");
            ///statement.executeUpdate("INSERT INTO Dane VALUES (3, 'Mickiewicz', 'Adam', 4.5)");
            ///statement.executeUpdate("INSERT INTO Dane VALUES (4, 'Kotek', 'Ludwik', 5.0)");
            ////////////////////////////Moje
            ///prst.setInt(1, 5);
            ///prst.setString(2, "Gowak");
            ///prst.setString(3, "Fan");
            ///prst.setFloat(4, (float)5.0);
            ///prst.executeUpdate();
            rs.moveToInsertRow();
            rs.updateInt(1, 5);
            rs.updateString(2, "YYYYY");
            rs.updateString(3, "ZZZZZ");
            rs.updateFloat(4, (float)6.0);
            rs.insertRow();
            rs.moveToCurrentRow();
            System.out.println("Data inserted");
        } catch (SQLException sqle) {
            System.err.println("SQL exception: " + sqle.getMessage());
        } catch (ClassNotFoundException cnfe) {
            System.err.println("ClassNotFound exception: " + cnfe.getMessage());
        } catch (Exception e) {
            System.err.println("Another exception: " + e.getMessage());
        } finally {
            try {
                if (con != null) {
                    con.close();
                }
            } catch (SQLException sqle) {
                System.err.println("SQL exception: " + sqle.getMessage());
            }
        }
    }

    public static void main(String[] args) {
        InsertDataApp insertDataApp = new InsertDataApp();
        insertDataApp.insertData();
    }
}
